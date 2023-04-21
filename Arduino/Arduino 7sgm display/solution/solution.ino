#include "funshield.h"
constexpr int buttons[] = { button1_pin, button2_pin, button3_pin };
constexpr int buttonsSize = sizeof(buttons) / sizeof(buttons[0]);
constexpr int moduloCounter = 10000;
constexpr int moduloPosition = 4;
constexpr byte segmentMap[] = 
{
  0xC0, // 0  0b11000000
  0xF9, // 1  0b11111001
  0xA4, // 2  0b10100100
  0xB0, // 3  0b10110000
  0x99, // 4  0b10011001
  0x92, // 5  0b10010010
  0x82, // 6  0b10000010
  0xF8, // 7  0b11111000
  0x80, // 8  0b10000000
  0x90  // 9  0b10010000
};
void displayDigit( byte digit, byte Pos)
{
  digitalWrite( latch_pin, LOW);
  shiftOut( data_pin, clock_pin, MSBFIRST, digit);
  shiftOut( data_pin, clock_pin, MSBFIRST, Pos);
  digitalWrite( latch_pin, HIGH);
}
class Digit
{
  public:
    Digit(int Pos, int Order)
    {
      pos = Pos;
      order = Order;
    }
    int order;
    void writeDigit(int* counter) 
    {
      int num = (*counter/order)%10;
      displayDigit( segmentMap[num], digit_muxpos[pos]);
    }
    private:
    int pos;
};

enum ButtonState{PRESSED, RELEASED};
enum ButtonType{INCREMENT, DECREMENT, ORDER};
int position = 0;
int counter;

class Button
{
  public:
    Button(ButtonType Type, Digit Digits[])
    {
      type = Type;
      digits = Digits;
      
    }
    void Pressed(bool is_pressed)
    {
      if (!is_pressed)
      {
        prev_state = RELEASED;
        return;
      }
      else if (prev_state == PRESSED)
      {
        return;
      }

      prev_state = PRESSED;
      changePositionDigit(type);
      digits[position].writeDigit(&counter);
    }
    private:
    ButtonState prev_state = RELEASED;
    ButtonType type;
    Digit *digits;

    void changePositionDigit(ButtonType Type)
    {
      switch (Type)
      {
        case INCREMENT:
          counter = (counter+digits[position].order)%moduloCounter;
          break;
        case DECREMENT:
          counter = (counter+moduloCounter-digits[position].order)%moduloCounter;
          break;
        case ORDER:
          position = (position + 1)%moduloPosition;
          break;
        default:
          break;
      }
    }
};

Digit digs[4] = { Digit(3, 1), Digit(2, 10), Digit(1, 100), Digit(0, 1000) };
Button buts[3] { Button(INCREMENT, digs), Button(DECREMENT, digs), Button(ORDER, digs) };
void setup() 
{
  for (int i = 0; i < buttonsSize; i++) 
  {
    pinMode(buttons[i], INPUT);
  }
  pinMode(latch_pin, OUTPUT);
  pinMode(data_pin, OUTPUT);
  pinMode(clock_pin, OUTPUT);
  displayDigit( segmentMap[0], digit_muxpos[3]);
}
void loop()
{
  for (int i = 0; i < buttonsSize; i++)
  {
    bool isPressed = !digitalRead(buttons[i]);
    buts[i].Pressed(isPressed);
  }
}