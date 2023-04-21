#include "funshield.h"
constexpr int leds[] = {led4_pin, led3_pin, led2_pin, led1_pin};
constexpr int ledsSize = sizeof(leds)/sizeof(leds[0]);
constexpr int WaitingTime = 1000;
constexpr long unsigned int Period = 300;
int counter = 0;
enum ButtonState{PRESSED, RELEASED};
enum ButtonType{INCREMENT, DECREMENT};
class Button
{
  public:
    Button(ButtonType Type)
    {
      type = Type; 
      period = WaitingTime; 
    }
    int Change(ButtonType Type)
    {
      switch (Type)
      {
        case INCREMENT:
          if (currentTime - lastTime >= period)
          {
            lastTime += period;
            period = Period;
            return 1;
          }
          else return 0;
          break;
        case DECREMENT:
          if (currentTime - lastTime >= period)
          {
            lastTime += period;
            period = Period;
            return -1;
          }
          else return 0;
          break;
        default:
          break;
      }
    }
    int Pressed(bool is_pressed)
    {
      if (!is_pressed) 
      {
        prev_state = RELEASED;
        period = WaitingTime;
        return 0;
      }
      if (prev_state == PRESSED)
      {
        currentTime = millis();
        return Change(type);
      }
      prev_state = PRESSED;
      currentTime = millis();
      lastTime = currentTime;
      Change(type);
      return (type == INCREMENT) ? 1 : -1;
    }
  private:
    ButtonState prev_state = RELEASED;
    ButtonType type;
    long unsigned int period;
    long unsigned int currentTime;
    long unsigned int lastTime;
};
void writeNumber(int number)
{
  for (int i = 0; i < ledsSize; i++)
  {
    ((number & 1) == 1) ? digitalWrite(leds[i], ON) : digitalWrite(leds[i], OFF);
    number = number >> 1;
  }
}
constexpr int buttons[] = { button1_pin, button2_pin, button3_pin };
Button Buttons[2] { Button(INCREMENT), Button(DECREMENT)};
constexpr int buttonsSize = sizeof(Buttons)/sizeof(Buttons[0]);
void setup() 
{
  for (int i = 0; i < ledsSize; i++)
  {
  pinMode(leds[i], OUTPUT);
  digitalWrite(leds[i], OFF);
  }
  for (int i = 0; i < buttonsSize; i++) 
  {
  pinMode(buttons[i], INPUT);
  }
}

void loop() {
  counter = (counter + 16) % 16;
  writeNumber(counter);
  for (int i = 0; i < buttonsSize; i++)
  {
    bool isPressed = !digitalRead(buttons[i]);
    counter += Buttons[i].Pressed(isPressed);
  }
}