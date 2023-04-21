#include "funshield.h"
long unsigned int lastTime;
long unsigned int timer;
long unsigned int period[2];
constexpr int leds[] = {led4_pin, led3_pin, led2_pin, led1_pin};
constexpr int ledsSize = sizeof(leds)/sizeof(leds[0]);
constexpr int buttons[] = {button1_pin, button2_pin, button3_pin};
constexpr int buttonsSize = sizeof(buttons)/sizeof(buttons[0]);
int currentNumber = 0;
bool lastButtonState1 = false;
bool lastButtonState2 = false;
int button1 = 0;
int button2 = 0;
unsigned long int last[2];
unsigned long currentTime = 0;
// setup is called just once at the beginning
void setup() 
{
  // Always use constants from funshield whenever possible...
  for (int i = 0; i < ledsSize; i++)
  {
  pinMode(leds[i], OUTPUT);
  digitalWrite(leds[i], OFF);
  }
  for (int i = 0; i < buttonsSize; i++)
  {
    pinMode(buttons[i], INPUT);
  }
  for (int i = 0; i < 2; i++)
  {
    last[i] = millis();
    period[i] = 1000;
  }
}
void function(int k)
    {
      for (int i = 0; i < ledsSize; i++)
      {
        if ((k & 1) == 1)
        {
          digitalWrite(leds[i], ON);
        }
        else
        {
          digitalWrite(leds[i], OFF);
        }
        k = k >> 1;
      }
    }
void counter(bool state1, bool state2)
{
  currentTime = millis();
    if ((currentTime - last[0] >= period[0]) & state1)
      {   
        last[0] += period[0];
        currentNumber++;
        period[0] = 300;
      }
    if ((currentTime - last[1] >= period[1]) & state2)
      {
        last[1] += period[1];
        currentNumber--;
        period[1] = 300;
      }
}
int detectChange(bool state, bool lastState, int j)
{
  if ((state != lastState) && (lastState == false))
  {
    if (j == 0)
      currentNumber += 1;
    else
      currentNumber -= 1;
    last[j] = millis();
    return 1;
  }
  else if ((state != lastState) && (lastState == true))
  {
    //last[j] = 0;
    period[j] = 1000;
    return -1;
  }
  else
  {
    return 0;
  }
}

// the main loop is called repeatedly by the bootstrap (main) code
void loop() {
  bool isPressed1 = !digitalRead(buttons[0]); 
  bool isPressed2 = !digitalRead(buttons[1]); 
  button1 += detectChange(isPressed1, lastButtonState1, 0);
  button2 += detectChange(isPressed2, lastButtonState2, 1);
  if ((button1 > 0) | (button2 > 0))
  {    
    counter(isPressed1, isPressed2);
  }
  function(currentNumber);
  lastButtonState1 = isPressed1;
  lastButtonState2 = isPressed2;
}