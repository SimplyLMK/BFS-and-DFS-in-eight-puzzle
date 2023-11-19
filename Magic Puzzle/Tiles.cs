using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Magic_Puzzle
{
    public abstract class Tiles
    {
        public enum Number
        {
            Empty = 0,
            One = 1,
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8
        }

        public Number Number_Order { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        public abstract void Draw(GUI_Grid gui, int x, int y);
    }



}
