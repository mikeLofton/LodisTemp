using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class UIText : Actor
    {
        public string Text;
        public int Width;
        public int Height;
        public int FontSize;
        public Font Font;
        public Color FontColor;

        public UIText(float x, float y, string name, Color color, int width, int height, int fontSize, string text = "") : base( x, y, name, "")
        {
            Text = text;
            Width = width;
            Height = height;
            Font = Raylib.LoadFont("resources/fonts/alagard.png");
            FontSize = fontSize;
            FontColor = color;
        }


        public override void Draw()
        {
            //Create a new rectangle that will act as the borders of the text box
            Rectangle textBox = new Rectangle(LocalPosition.X, LocalPosition.Y, Width, Height);
            //Draw text box 
            Raylib.DrawTextRec(Font, Text, textBox, FontSize, 1, true, FontColor);
        }
    }
}
