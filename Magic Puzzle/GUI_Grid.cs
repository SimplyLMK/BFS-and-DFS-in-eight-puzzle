using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic_Puzzle
{
    public class GUI_Grid // this class handle GUI aspect of the grid
    {
        public int Rows = 3;
        public int Columns = 3;
        private int _size_of_square;

        public int Square_Size
        {
            get { return _size_of_square; }
            set { _size_of_square = value; }
        }

        

        
        public void Draw(Grid grid)
        {
            //using a nested loop to iterate through the 2d array

            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    int x = col * Square_Size;
                    int y = row * Square_Size;

                    if (grid.position[row, col] != null)
                    {
                        grid.position[row, col].Draw(this, x, y); 
                    }
                }
            }
        }


        public bool Clicked(Rectangle box)
        {
            return SplashKit.MouseClicked(MouseButton.LeftButton) && SplashKit.PointInRectangle(SplashKit.MousePosition(), box);
        }

        Rectangle BFS = new Rectangle { X = 70, Y = 100, Width = 150, Height = 20 };

        

    }
}
