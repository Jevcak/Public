#include "funshield.h"
constexpr byte d = 0b10100001;   // d
enum State {NORMAL, CONF};
enum ButtonState{PRESSED, HELD, RELEASED};
constexpr int moduloDigit = 4;
constexpr byte whiteSpace = 0xFF;
int GetThePowerOf(int num, int base)
{
  int tmp = 0;
  while (num >= base)
  {
    num = num / base;
    tmp++;
  }
  return tmp;
}
class Digit
{
  public:
    Digit(int Order)
    {
      order = Order;
      position = digit_muxpos[Positions[GetThePowerOf(Order, 10)]];
    }
    int position;
    int order;
    int Positions[moduloDigit] {3, 2, 1, 0};
};

class Button
{
  public:
    int pin;
    Button(int Pin)
    {
      pin = Pin;
    }
    ButtonState Pressed()
    {
      bool is_pressed = !digitalRead(pin);
      if (is_pressed & (prev_state == RELEASED))
      {
        prev_state = PRESSED;
        return PRESSED;
      }
      else if (!is_pressed)
      {
        prev_state = RELEASED;
        return RELEASED;
      }
      return HELD;
    }
    private:
    ButtonState prev_state = RELEASED;
};
Digit digs[moduloDigit] = { Digit(1), Digit(10), Digit(100), Digit(1000) };
Button Buttons[3] { Button(button1_pin), Button(button2_pin), Button(button3_pin) };
constexpr int ButtonsSize = sizeof(Buttons) / sizeof(Buttons[0]);
class Display
{
  public:
    Display(int which)
    {
      digit = which;
    }
    void DisplayWhatIsGiven(byte WhatToShow[4])
    {

    }
     void WhichDigitandWhat(int number)
    {
      int res = digits[(number/digs[digit].order)%10];
      if ((number <= digs[digit].order) && (digit >= 2))
      {
        res = whiteSpace;
      }
      displayDigit(res);
      digit++; 
      digit %= moduloDigit;
    }
    void displayDigit( byte Digit)
    {
      shiftOut( data_pin, clock_pin, MSBFIRST, Digit);
      shiftOut( data_pin, clock_pin, MSBFIRST, digs[digit].position);
      digitalWrite( latch_pin, LOW);
      digitalWrite( latch_pin, HIGH);
    }
  private:
    int digit;
};
Display display(0);
class Dice
{
  public:
    Dice()
    {

    }
  private:
    int modThrows = 10;
    enum Type {d4, d6, d8, d10, d12, d20, d100};
};
State current = CONF;

void setup() 
{
  for (int i = 0; i < ButtonsSize; i++) 
  {
    pinMode(Buttons[i].pin, INPUT);
  }
  pinMode(latch_pin, OUTPUT);
  pinMode(data_pin, OUTPUT);
  pinMode(clock_pin, OUTPUT);
}
void loop() 
{
  switch(current)
  {
    case NORMAL:
    {
      if ((Buttons[1].Pressed() == PRESSED) | (Buttons[2].Pressed() == PRESSED))
      {
        current = CONF;
      }
      display.WhichDigitandWhat(5432);
      break;

    }
    case CONF:
    {
      if (Buttons[0].Pressed() == PRESSED)
      {
        current = NORMAL;
      }
      display.WhichDigitandWhat(4444);
    }
  }
}
