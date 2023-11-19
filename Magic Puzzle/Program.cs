using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Magic_Puzzle
{
    public class Program
    {
        public static void Main()
        {
             M_Puzzle game1 = new M_Puzzle();
             game1.Run(game1);

            //feel free to comment out the menu functionality if you just want to solve the puzzle
           // Menu menu = Menu.Instance;
          //  menu.Run(game1);

        }
    }
}
