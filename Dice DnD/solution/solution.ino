#include "funshield.h"
constexpr byte d = 0b10100001;   // d


class Button
{
  public:
    int pin;
    Button(int Pin)
    {
      pin = Pin;
    }
    bool Pressed()
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
    enum ButtonState{PRESSED, HELD, RELEASED};
    ButtonState prev_state = RELEASED;
};
Button Buttons[3] { Button(button1_pin), Button(button2_pin), Button(button3_pin) };

void setup() {
  // put your setup code here, to run once:

}

void loop() {
  // put your main code here, to run repeatedly:

}
