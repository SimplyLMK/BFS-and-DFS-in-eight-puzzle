using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace Magic_Puzzle
{
    public class Tile_Mover
    {
        private Grid _grid;
        private M_Puzzle _game;

        public Tile_Mover(Grid grid, M_Puzzle game)
        {
            _grid = grid;
            _game = game;
        }

        //take in the solution list solved by searchAlgorithm and move accordingly
        public void Move_Tiles(List<Node> path) 
        {
            int numMoves = path.Count;

            for (int i = 0; i < numMoves - 1; i++)
            {
                Node currentNode = path[i];
                Node nextNode = path[i + 1];

                int emptyRow = currentNode.emptyRow;
                int emptyCol = currentNode.emptyCol;

                int tileRow = nextNode.emptyRow;
                int tileCol = nextNode.emptyCol;

                SwapTiles(tileRow, tileCol, emptyRow, emptyCol);
                _grid.EmptyRow = tileRow;
                _grid.EmptyCol = tileCol;

                _game.Update();
                _game.Draw();
                Thread.Sleep(500);
            }

            // Move from the last state in the path to the goal state
            Node lastNode = path[numMoves - 1];
            int lastEmptyRow = lastNode.emptyRow;
            int lastEmptyCol = lastNode.emptyCol;

            int goalEmptyRow = _grid.GoalEmptyRow;
            int goalEmptyCol = _grid.GoalEmptyCol;

            SwapTiles(goalEmptyRow, goalEmptyCol, lastEmptyRow, lastEmptyCol);
            _grid.EmptyRow = goalEmptyRow;
            _grid.EmptyCol = goalEmptyCol;

            _game.Update();
            _game.Draw();
            Thread.Sleep(500);
        }



        // same as SwapTiles in M_puzzle class. However, the usage of inheritance
        // is not logical since its not a 'is a' relationship between the 2 classes
        private void SwapTiles(int row1, int col1, int row2, int col2)
        {
            Tiles temp = _grid.position[row1, col1];
            _grid.position[row1, col1] = _grid.position[row2, col2];
            _grid.position[row2, col2] = temp;

            if (_grid.position[row1, col1] != null)
            {
                _grid.position[row1, col1].Row = row1;
                _grid.position[row1, col1].Col = col1;
            }

            if (_grid.position[row2, col2] != null)
            {
                _grid.position[row2, col2].Row = row2;
                _grid.position[row2, col2].Col = col2;
            }

            _grid.EmptyRow = row2;
            _grid.EmptyCol = col2;
        }
    }
}
