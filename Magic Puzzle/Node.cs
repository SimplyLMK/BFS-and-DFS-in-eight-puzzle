using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic_Puzzle
{
    public class Node
    {
        public List<Node> children = new List<Node>();
        public Node parent;
        public int[,] puzzle = new int[3, 3];
        public int emptyRow;
        public int emptyCol;

        public Node(int[,] p)
        {
            Set_Puzzle(p);
        }

        public void Set_Puzzle(int[,] p)
        {
            for (int i = 0; i < puzzle.GetLength(0); i++)
            {
                for (int j = 0; j < puzzle.GetLength(1); j++)
                {
                    puzzle[i, j] = p[i, j];
                    if (p[i, j] == 0)
                    {
                        emptyRow = i;
                        emptyCol = j;
                    }
                }
            }
        }

        public bool GoalState()
        {
            int[,] arr = {
            { 0, 1, 2 },
            { 3, 4, 5 },
            { 6, 7, 8 }
        };

            return Enumerable.Range(0, 3)
                .All(row => Enumerable.Range(0, 3)
                    .All(col => puzzle[row, col] == arr[row, col]));
        }

        public void Expand_Node()
        {
            Move_To_Right(emptyRow, emptyCol);
            Move_To_Left(emptyRow, emptyCol);
            Move_Up(emptyRow, emptyCol);
            Move_Down(emptyRow, emptyCol);
        }

        public void Move_To_Right(int row, int col)
        {
            if (col < 2)
            {
                int[,] pc = Copy_Puzzle();
                int temp = pc[row, col + 1];
                pc[row, col + 1] = pc[row, col];
                pc[row, col] = temp;
                Node child = new Node(pc);
                children.Add(child);
                child.parent = this;
            }
        }

        public void Move_To_Left(int row, int col)
        {
            if (col > 0)
            {
                int[,] pc = Copy_Puzzle();
                int temp = pc[row, col - 1];
                pc[row, col - 1] = pc[row, col];
                pc[row, col] = temp;
                Node child = new Node(pc);
                children.Add(child);
                child.parent = this;
            }
        }

        public void Move_Up(int row, int col)
        {
            if (row > 0)
            {
                int[,] pc = Copy_Puzzle();
                int temp = pc[row - 1, col];
                pc[row - 1, col] = pc[row, col];
                pc[row, col] = temp;
                Node child = new Node(pc);
                children.Add(child);
                child.parent = this;
            }
        }

        public void Move_Down(int row, int col)
        {
            if (row < 2)
            {
                int[,] pc = Copy_Puzzle();
                int temp = pc[row + 1, col];
                pc[row + 1, col] = pc[row, col];
                pc[row, col] = temp;
                Node child = new Node(pc);
                children.Add(child);
                child.parent = this;
            }
        }

        public int[,] Copy_Puzzle()
        {
            int[,] copy = new int[3, 3];
            for (int i = 0; i < puzzle.GetLength(0); i++)
            {
                for (int j = 0; j < puzzle.GetLength(1); j++)
                {
                    copy[i, j] = puzzle[i, j];
                }
            }
            return copy;
        }

        public bool SameP(int[,] p)
        {
            for (int i = 0; i < puzzle.GetLength(0); i++)
            {
                for (int j = 0; j < puzzle.GetLength(1); j++)
                {
                    if (puzzle[i, j] != p[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }



        public void Print_Console()
        {
            for (int i = 0; i < puzzle.GetLength(0); i++)
            {
                for (int j = 0; j < puzzle.GetLength(1); j++)
                {
                    Console.Write(puzzle[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
