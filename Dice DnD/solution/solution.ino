#include "funshield.h"
constexpr byte d = 0b10100001;   // d
enum State {NORMAL, CONF};
enum ButtonState{PRESSED, HELD, RELEASED, INACTIVE};
constexpr int moduloDigit = 4;
constexpr byte whiteSpace = 0xFF;
bool ledsOn = false;
bool change = false;
class Leds 
{
  public:
    int j = 0;
    void Setup()
    {
      for (int i = 0; i < ledArraySize; i++)
      {
        pinMode(ledArray[i], OUTPUT);
        digitalWrite(ledArray[i], OFF);
      }
    }
    void PatternGo()
    {
      currentTime = millis();
      if (currentTime - lastTime >= PERIOD)
      { 
        digitalWrite(ledArray[pattern[j]], OFF);
        j++;
        j = j % 6;
        digitalWrite(ledArray[pattern[j]], ON);
        lastTime += PERIOD;
      }
    }
  private:
    int ledArray[4] {led1_pin, led2_pin, led3_pin, led4_pin};
    int pattern[6] {0, 1, 2, 3, 2, 1};
    unsigned long currentTime;
    unsigned long lastTime;
    int PERIOD = 50;
    int ledArraySize = sizeof(ledArray) / sizeof(ledArray[0]);
};
Leds leds;
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
      if (is_pressed && (prev_state == RELEASED) | (prev_state == INACTIVE))
      {
        prev_state = PRESSED;
        return PRESSED;
      }
      else if ((!is_pressed) && (prev_state == RELEASED))
      {
        prev_state = INACTIVE;
        return INACTIVE;
      }
      else if ((!is_pressed) && (prev_state == PRESSED))
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
    void DisplayConf(int throws, int type)
    {
      if (digit == 3)
      {
        displayDigit(digits[throws]);
      }
      else if (digit == 2)
      {
        displayDigit(d);
      }
      else
      {
        displayDigit(digits[(type/digs[digit].order)%10]);
      }
      digit++; 
      digit %= moduloDigit;
    }
     void WhichDigitandWhat(int number)
    {
      int res = digits[(number/digs[digit].order)%10];
      if ((number <= digs[digit].order) && (digit != 0))
      {
        res = whiteSpace;
      }
      displayDigit(res);
      digit++; 
      digit %= moduloDigit;
    }
    void displayDigit( byte Glyph)
    {
      shiftOut( data_pin, clock_pin, MSBFIRST, Glyph);
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
    int NumberOfThrows = 0;
    int modThrows = 9;
    int modTypes = 7;
    int WhichType = 0;
    bool generate = false;
    int Type[7] {4, 6, 8, 10, 12, 20, 100};
    void Throwing()
    {
      CurrentTime = millis();
      ButtonState FirstButtonPressed = Buttons[0].Pressed();
      if (FirstButtonPressed == PRESSED)
      {
        LastTime = CurrentTime;
        leds.j = 0;
        ledsOn = true;
      }
      else if (FirstButtonPressed == RELEASED)
      {
        if (change)
        {
          generate = true;
        }
        ledsOn = false;
        change = true;
      }
    }
    void Generate()
    {
      if (generate)
      {
        unsigned long seed = CurrentTime - LastTime;
        randomSeed(seed);
        int temp = 0;
        int temp_sum = 0;
        for (int i = 0; i < NumberOfThrows + 1; i++)
        {
          temp = random(1, Type[WhichType] + 1);
          temp_sum += temp;
        }
        generate = false;
        result = temp_sum;
      }
    }
    int result;
  private:
    unsigned int CurrentTime;
    unsigned int LastTime;
};
State current = CONF;
Dice dice;
void setup() 
{
  for (int i = 0; i < ButtonsSize; i++) 
  {
    pinMode(Buttons[i].pin, INPUT);
  }
  pinMode(latch_pin, OUTPUT);
  pinMode(data_pin, OUTPUT);
  pinMode(clock_pin, OUTPUT);
  leds.Setup();
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
      (ledsOn) ? leds.PatternGo() : leds.Setup();
      dice.Throwing();
      if (change)
      {
       dice.Generate();
      }
      display.WhichDigitandWhat(dice.result);
      break;
    }
    case CONF:
    {
      if (Buttons[0].Pressed() == PRESSED)
      {
        current = NORMAL;
        change = false;
      }
      if (Buttons[1].Pressed() == PRESSED)
      {
        dice.NumberOfThrows++;
        dice.NumberOfThrows %= dice.modThrows;
      }
      if (Buttons[2].Pressed() == PRESSED)
      {
        dice.WhichType++;
        dice.WhichType %= dice.modTypes;
      }
      display.DisplayConf(dice.NumberOfThrows + 1, dice.Type[dice.WhichType]);
    }
  }
}
