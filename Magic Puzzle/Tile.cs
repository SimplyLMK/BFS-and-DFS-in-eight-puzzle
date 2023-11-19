using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Magic_Puzzle
{
    public class Tile : Tiles
    {
        public Tile(Number numberOrder, int row, int col) // constructor initializing number enum on row and column
        {
            Number_Order = numberOrder;
            Row = row;
            Col = col;
        }

        public override void Draw(GUI_Grid gui, int x, int y) // override the abstract draw method from abstract Tiles class
        {
            int size = gui.Square_Size;

            if (Number_Order == Tiles.Number.Empty) // if is empty enum than draw white tile
            {
                SplashKit.FillRectangle(Color.White, x, y, size, size);
            }
            else
            {
                SplashKit.FillRectangle(Color.LightGray, x, y, size, size); // else draw the gray tiles

                float centerX = x + size / 2;
                float centerY = y + size / 2;

                SplashKit.DrawText(((int)Number_Order).ToString(), Color.Black, centerX, centerY); // draw the number in the center of the square tile
            }

            SplashKit.DrawRectangle(Color.Black, x, y, size, size);
        }


       


    }

    


}
