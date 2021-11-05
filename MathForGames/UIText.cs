using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;

namespace MathForGames
{
    class UIText : Actor
    {
        public string Text;
        public int Width;
        public int Height;

        public UIText(float x, float y, string name, ConsoleColor color, int width, int height, string text = "") : base('\0', x, y, name, color)
        {
            Text = text;
            Width = width;
            Height = height;
        }


        public override void Draw()
        {
            //Store the position of the cursor
            int cursorPosX = (int)Position.X;
            int cursorPosY = (int)Position.Y;

            //Create a new icon to store the current character and color
            Icon currentLetter = new Icon { Color = Icon.Color };

            //Convert the string for text into a character array
            char[] textChars = Text.ToCharArray();

            //Iterate through all characters in the string
            for (int i = 0; i < textChars.Length; i++)
            {
                //Set the icon symbol to be the current character in the array
                currentLetter.Symbol = textChars[i];

                if (currentLetter.Symbol =='\n')
                {
                    cursorPosX = (int)Position.X;
                    cursorPosY++;
                    continue;
                }

                //Add the current character to the buffer
                Engine.Render(currentLetter, new Vector2 { X = cursorPosX, Y = cursorPosY });

                //Increment the cursor position so the letters are set side by side
                cursorPosX++;

                //Go to the next line if the cursor has reached the max position
                if (cursorPosX - (int)Position.X > Width)
                {
                    //Reset the cursor x position and increase the y position
                    cursorPosX = (int)Position.X;
                    cursorPosY++;
                }
                //If the cursor has reached the maximum height...
                if (cursorPosY - (int)Position.Y > Height)
                    //...leave the loop
                    break;
            }
        }
    }
}
