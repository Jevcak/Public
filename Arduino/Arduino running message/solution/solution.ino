#include "funshield.h"
#include "input.h"

// map of letter glyphs
constexpr byte LETTER_GLYPH[] {
  0b10001000,   // A
  0b10000011,   // b
  0b11000110,   // C
  0b10100001,   // d
  0b10000110,   // E
  0b10001110,   // F
  0b10000010,   // G
  0b10001001,   // H
  0b11111001,   // I
  0b11100001,   // J
  0b10000101,   // K
  0b11000111,   // L
  0b11001000,   // M
  0b10101011,   // n
  0b10100011,   // o
  0b10001100,   // P
  0b10011000,   // q
  0b10101111,   // r
  0b10010010,   // S
  0b10000111,   // t
  0b11000001,   // U
  0b11100011,   // v
  0b10000001,   // W
  0b10110110,   // ksi
  0b10010001,   // Y
  0b10100100,   // Z
};
constexpr byte EMPTY_GLYPH = 0b11111111;

constexpr int positionsCount = 4;
constexpr unsigned int scrollingInterval = 300;
constexpr int spacesAfter = 1;
constexpr int spacesBefore = 3;
/** 
 * Show chararcter on given position. If character is not letter, empty glyph is displayed instead.
 * @param ch character to be displayed
 * @param pos position (0 = leftmost)
 */
void displayChar(char ch, byte pos)
{
  byte glyph = EMPTY_GLYPH;
  if (isAlpha(ch)) {
    glyph = LETTER_GLYPH[ ch - (isUpperCase(ch) ? 'A' : 'a') ];
  }
  
  digitalWrite(latch_pin, LOW);
  shiftOut(data_pin, clock_pin, MSBFIRST, glyph);
  shiftOut(data_pin, clock_pin, MSBFIRST, 1 << pos);
  digitalWrite(latch_pin, HIGH);
}
class Display
{
  public:
    const char* message = nullptr;
    bool after = false;
    bool MessageRead = true;
    Display()
    {
      position = 0;
      movement = 0;
    }
    void SetMessage(const char* FromInput)
    {
      if (MessageRead)
      {
        message = FromInput;
        message = AddSpaces();
        MessageRead = false;
        chars = WhatToDisplay();
      }
    }
    void DisplayMessage()
    {
      Scroll();
      if ((chars[position]!='\0') && !after)
      {
        displayChar(chars[position], position);
      }
      else
      {
        after = true;
        displayChar(' ',position);
      }
      if (position==3) after = false;
      position++;
      position%=positionsCount;
    }
    int GetLength(const char* String)
    {
      int l = 0;
      while(String[l])
      {
        l++;
      }
      return l;
    }
    const char* AddSpaces()
    {
      char* temp = new char[GetLength(message)+spacesBefore+spacesAfter];
      int i = 0;
      for (int j = 0; j < spacesBefore; j++)
      {
        temp[j] = ' ';
      }
      while (message[i])
      {
        temp[i+spacesBefore] = message[i];
        i++;
      }
      for (int k = 0; k < spacesAfter; k++)
      {
        temp[k+i+spacesBefore] = ' ';
      }
      
      temp[spacesBefore+i+spacesAfter+1] = '\0';
      return &temp[0];
    }
  private:
    const char* WhatToDisplay()
    {
      const char* pointer = &message[movement];
      if (message[movement]) 
      {
        return pointer;
      }
      else
      {
        movement = 0;
        MessageRead = true;
        delete message;
        return pointer;
      }
    }
    void Scroll()
    {
      CurrentTime = millis();
      if (CurrentTime - LastTime >= scrollingInterval)
      {
        movement++;
        chars = WhatToDisplay();
        LastTime += scrollingInterval;
      }
    }
    int position;
    int movement;
    const char* chars;
    unsigned int CurrentTime;
    unsigned int LastTime;
};
SerialInputHandler input;
void setup() {
  pinMode(latch_pin, OUTPUT);
  pinMode(clock_pin, OUTPUT);
  pinMode(data_pin, OUTPUT);
  input.initialize();
}
Display display;
void loop() {
  input.updateInLoop();
  display.SetMessage(input.getMessage());
  display.DisplayMessage();
}