using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;


namespace Magic_Puzzle
{
    public class M_Puzzle
    {
        private Grid _grid;
        private GUI_Grid _gui;
        private Tile_Mover _tileMover;
        public int moves = 0;
        public int highest_score;


        public int Moves // implement the function to save and override the highest score in HD project
        {
            get { return moves; }
            set { moves = value; }
        }


        public M_Puzzle()
        {
            _gui = new GUI_Grid();
            _grid = new Grid(_gui);
            _tileMover = new Tile_Mover(_grid, this);
        }

        public int[,] GetGridState()
        {
            int[,] gridState = new int[_gui.Rows, _gui.Columns];

            for (int row = 0; row < _gui.Rows; row++)
            {
                for (int col = 0; col < _gui.Columns; col++)
                {
                    if (_grid.position[row, col] != null)
                    {
                        gridState[row, col] = (int)_grid.position[row, col].Number_Order;
                    }
                    else
                    {
                        gridState[row, col] = 0; 
                    }
                }
            }

            return gridState;
        }


        private void Print_Console() // print move to console
        {
            moves++;
            Console.WriteLine($"The total moves are: {moves}");    
            Console.WriteLine("Current Position:");

            // using a nested for loop, iterate horizontally and vertically to check position of the instances and print out to console. If not null that is.
            for (int row = 0; row < _gui.Rows; row++)
            {
                for (int col = 0; col < _gui.Columns; col++)
                {
                    if (_grid.position[row, col] != null)
                    {
                        Console.Write(" " + (int)_grid.position[row, col].Number_Order);
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }

                Console.WriteLine();
            }
        }



        private void HandleInput()
        {
            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                int mouseX = (int)SplashKit.MouseX();
                int mouseY = (int)SplashKit.MouseY();

                // Check if the click is within the grid boundaries
                if (mouseX >= 0 && mouseX < _gui.Square_Size * _gui.Columns &&
                    mouseY >= 0 && mouseY < _gui.Square_Size * _gui.Rows)
                {
                    int row = (int)(mouseY / _gui.Square_Size);
                    int col = (int)(mouseX / _gui.Square_Size);

                    Tiles clickedTile = _grid.position[row, col];

                    if (clickedTile != null)
                    {
                        if (clickedTile.Number_Order == Tiles.Number.Empty)
                        {
                            _grid.EmptyRow = row;
                            _grid.EmptyCol = col;

                            Print_Console();
                        }
                        else if (Is_Adjacent_empty(row, col))
                        {
                            SwapTiles(row, col, _grid.EmptyRow, _grid.EmptyCol);
                            _grid.EmptyRow = row;
                            _grid.EmptyCol = col;
                            Print_Console();

                            if (_grid.Completed())
                            {
                                Console.WriteLine("Congratulations! You won the game!");
                               
                            }
                        }
                    }
                }
            }
        }



        private bool Is_Adjacent_empty(int row, int col) // check if a square is adjacent to the empty square to permit tile swapping
        {
            int emptyRow = _grid.EmptyRow;
            int emptyCol = _grid.EmptyCol;

            return (row == emptyRow && Math.Abs(col - emptyCol) == 1) ||
                   (col == emptyCol && Math.Abs(row - emptyRow) == 1);
        }

        public void Update()
        {
            SplashKit.RefreshScreen();
        }

        public void Draw()
        {
            _gui.Draw(_grid);
        }


        private void SwapTiles(int row1, int col1, int row2, int col2) // Handles the swapping of two tiles in the grid
        {
            Tiles temp = _grid.position[row1, col1]; // Create a temporary instance of Tile class representing the tile at position (row1, col1)
            _grid.position[row1, col1] = _grid.position[row2, col2]; // Assign the tile at position (row2, col2) to the old position in the grid
            _grid.position[row2, col2] = temp; // Assign the original tile(temp) to the position (row2, col2)

            if (_grid.position[row1, col1] != null)
            {
                // Update the Row and Col properties of the tile at position row1 col1
                _grid.position[row1, col1].Row = row1;
                _grid.position[row1, col1].Col = col1;
            }

            if (_grid.position[row2, col2] != null)
            {
                // Update the Row and Col properties of the tile at position row2 cole2
                _grid.position[row2, col2].Row = row2;
                _grid.position[row2, col2].Col = col2;
            }

            _grid.EmptyRow = row2; // Update the EmptyRow property to display new position of empty tile.
            _grid.EmptyCol = col2; // Same
        }



        public void Run(M_Puzzle game1) // activate the m_puzzle game
        {
            _gui.Square_Size = 100;

            //SplashKit.OpenWindow("Magic Puzzle", _gui.Square_Size * _gui.Columns, _gui.Square_Size * _gui.Rows);
            SplashKit.OpenWindow("Magic Puzzle", 400, 300);
            SplashKit.ClearScreen(Color.White);

            bool Clicked(Rectangle box)
            {
                return SplashKit.MouseClicked(MouseButton.LeftButton) && SplashKit.PointInRectangle(SplashKit.MousePosition(), box);
            }

            Rectangle BFS = new Rectangle { X = 323, Y = 35, Width = 60, Height = 30 };
            Rectangle DFS = new Rectangle { X = 323, Y = 135, Width = 60, Height = 30 };
            SplashKit.FillRectangle(Color.SwinburneRed, BFS);
            SplashKit.FillRectangle(Color.SwinburneRed, DFS);
            SplashKit.DrawText("BFS", Color.BrightGreen, 340, 47);
            SplashKit.DrawText("DFS", Color.SpringGreen, 340, 147);

            int[,] puzzle = game1.GetGridState();

            int[,] sample_p =
            {
            { 1, 0, 2 },
            { 3, 4, 5 },
            { 6, 7, 8 }
            };

            int[,] sample_p2 =
            {
            { 1, 2, 3 },
            { 4, 0, 6 },
            { 7, 5, 8 }
            };

            Node root = new Node(puzzle);

            bool isRunning = true;
            while (isRunning)
            {

                SplashKit.ProcessEvents();
                HandleInput();
                Update();
                Draw();
                if (Clicked(BFS))
                {
                    Console.WriteLine("Breadth First Search activated, hang on ...");

                    DateTime S_time = DateTime.Now;

                    BFS ui = new BFS();
                    List<Node> sol = ui.Search(root);

                    DateTime E_time = DateTime.Now;
                    TimeSpan t = E_time - S_time;

                    if (sol.Count > 0)
                    {
                        for (int i = 0; i < sol.Count; i++)
                        {
                            sol[i].Print_Console();
                        }
                        _tileMover.Move_Tiles(sol);
                    }
                    else
                    {
                        Console.WriteLine("No path found");
                    }
                    Console.WriteLine($"Time taken: {t.TotalMinutes}(m)");
                    Console.ReadLine();
                }
                if (Clicked(DFS))
                {
                    Console.WriteLine("Depth-First Search activated, hang on ...");

                    DateTime S_time = DateTime.Now;

                    DFS ui = new DFS();
                    List<Node> sol = ui.Search(root);

                    DateTime E_time = DateTime.Now;
                    TimeSpan t = E_time - S_time;

                    if (sol.Count > 0)
                    {
                        for (int i = 0; i < sol.Count; i++)
                        {
                            sol[i].Print_Console();
                        }
                        _tileMover.Move_Tiles(sol);
                    }
                    else
                    {
                        Console.WriteLine("No path found");
                    }
                    Console.WriteLine($"Time taken: {t.TotalMinutes}(m)");
                    Console.ReadLine();
                }

                if (SplashKit.WindowCloseRequested("Magic Puzzle") || SplashKit.QuitRequested())
                    isRunning = false;
            }

            SplashKit.CloseWindow("Magic Puzzle");
        }



    }




}