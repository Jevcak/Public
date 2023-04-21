#include "funshield.h"
unsigned long lastTime;
unsigned long currentTime;
constexpr int PERIOD = 300;
constexpr int array[] = {led1_pin, led2_pin, led3_pin, led4_pin};
constexpr int pattern[] = {0, 1, 2, 3, 2, 1};
// setup is called just once at the beginning
void setup() 
{
  for (int i = 0; i < 4; i++)
  {
  pinMode(array[i], OUTPUT);
  digitalWrite(array[i], OFF);
  }
  lastTime = millis();
}
int j = 0;
void loop() {
  currentTime = millis();
  if (currentTime - lastTime >= PERIOD)
  { 
  digitalWrite(array[pattern[j]], OFF);
  j++;
  j = j % 6;
  digitalWrite(array[pattern[j]], ON);
  lastTime += PERIOD;
  }
}