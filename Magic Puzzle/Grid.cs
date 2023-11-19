using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Magic_Puzzle
{
    public class Grid // this class handle the logic of the grid
    {
        public Tiles[,] position; // create a 2d array act as the basis for the grid.
        private readonly GUI_Grid _gui;
        private Random _random;

        //properties:
        public int EmptyRow { get; set; }
        public int EmptyCol { get; set; }

        public int GoalEmptyRow { get; } 
        public int GoalEmptyCol { get; } 


        public Grid(GUI_Grid gui) // constructor
        {
            position = new Tiles[3, 3]; // initialize 2d array with size 3x3
            _gui = gui;
            _random = new Random(); 

            //nested loop iterate row then column to set the initial pos to null
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    position[row, col] = null;
                }
            }
            
            position[0, 0] = new Tile(Tiles.Number.One, 0, 0);
            position[0, 1] = new Tile(Tiles.Number.Two, 0, 1);
            position[0, 2] = new Tile(Tiles.Number.Three, 0, 2);
            position[1, 0] = new Tile(Tiles.Number.Four, 1, 0);
            position[1, 1] = new Tile(Tiles.Number.Empty, 1, 1);
            position[1, 2] = new Tile(Tiles.Number.Six, 1, 2);
            position[2, 0] = new Tile(Tiles.Number.Seven, 2, 0);
            position[2, 1] = new Tile(Tiles.Number.Five, 2, 1);
            position[2, 2] = new Tile(Tiles.Number.Eight, 2, 2);
            
           

           // RandomizeTiles(); // call the method to create the randomized tile on the empty grid
        }

        public void RandomizeTiles() // randomize method
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (row == EmptyRow && col == EmptyCol)                                 
                    {
                        position[row, col] = new Tile(Tiles.Number.Empty, row, col); // create a new Tile instance with the number "Empty" and assign it to the position
                    }
                    else
                    {
                        int randomIndex = _random.Next(numbers.Count); // create a random index with the remainning number
                        int number = numbers[randomIndex]; // get the number at random index
                        numbers.RemoveAt(randomIndex); // discard to avoid repetition
                        position[row, col] = new Tile((Tiles.Number)number, row, col);
                    }
                }
            }
        }
        /*
        student note: 
         randomize method does not have the implemented logic to check for 
         puzzle inversion state (which is when the puzzle is unsolvable due to the initial configuration)
         therefore, it is advisable to to use the pre-defined position as its easy to solve in a couple of moves
         for auto-solving and wanting to check win condition.
        */

        // Handling winning logic.
        public bool Completed() // the goal is to allign 1 2 3 ; 4 5 6; 7 8 0.
        {
            int expectedNumber = 1; // base line to check against

            // Using a nested for loop to iterate through the grid
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (row == 2 && col == 2) // at position 2,2 which is the corner last position, check if its empty
                    {
                        if (position[row, col].Number_Order != Tiles.Number.Empty)
                        {
                            // If not empty then the puzzle is not completed
                            return false;
                        }
                    }
                    else if (position[row, col].Number_Order != (Tiles.Number)expectedNumber)
                    {
                        // If current position is different to the expected number then the puzzle is not completed
                        return false;
                    }

                    expectedNumber++; // Increment the expected number for the next iteration
                }
            }

            return true; // If all positions pass the validation, the puzzle is completed.
        }




    }
}
