using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic_Puzzle
{
    public class DFS : SearchAlgorithm
    {

        // the logic of how BFS and DFS work is similar, the only difference is
        // BFS uses a queque while DFS use the stack storage logic
        public override List<Node> Search(Node root)
        {
            List<Node> path_to_solution = new List<Node>();
            Stack<Node> frontier = new Stack<Node>(); // Stack to store nodes to visit (LIFO) rule
            List<Node> visited = new List<Node>();
            frontier.Push(root);
            bool goal_found = false;

            while (frontier.Count > 0 && !goal_found)
            {
                Node current_node = frontier.Pop(); // Visit the next node in the stack
                visited.Add(current_node);

                if (current_node.GoalState())
                {
                    Console.WriteLine("Goal Found");
                    goal_found = true;
                    Path_Tracer(path_to_solution, current_node);
                    break;
                }

                current_node.Expand_Node();
                //current_node.Print_Console();

                for (int i = 0; i < current_node.children.Count; i++)
                {
                    Node current_child = current_node.children[i];
                    if (!Contains(frontier.ToList(), current_child) && !Contains(visited, current_child))
                    {
                        frontier.Push(current_child);
                    }
                }
            }

            return path_to_solution;
        }
    }
}
