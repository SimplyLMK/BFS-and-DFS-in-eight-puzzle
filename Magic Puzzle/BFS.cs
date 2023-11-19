using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic_Puzzle
{
    public class BFS : SearchAlgorithm
    {

        // Override the abstract Search method for Breadth-First Search
        // the logic of how BFS and DFS work is similar, the only difference is
        // BFS uses a queque while DFS use the stack storage logic
        public override List<Node> Search(Node root)
        {
            List<Node> path_to_solution = new List<Node>(); // List to store the path to the solution
            Queue<Node> frontier = new Queue<Node>(); // Queue to store nodes to visit (FIFO) rule
            List<Node> visited = new List<Node>();  // List to store visited nodes
            frontier.Enqueue(root); // enqueue as a queque
            bool goal_found = false;

            // continue finding until reached goal state
            while (frontier.Count > 0 && !goal_found)
            {
                // visit next node and mark current and visited
                Node current_node = frontier.Dequeue();
                visited.Add(current_node);

                //if goal found
                if (current_node.GoalState())
                {
                    Console.WriteLine("Goal Found");
                    goal_found = true;
                    Path_Tracer(path_to_solution, current_node);
                    break;
                }

                current_node.Expand_Node(); // Generate the children of the current node
                //current_node.Print_Console();

                for (int i = 0; i < current_node.children.Count; i++)
                {
                    // If the child is not in the frontier or the visited list
                    Node current_child = current_node.children[i];
                    if (!Contains(frontier.ToList(), current_child) && !Contains(visited, current_child))
                    {
                        frontier.Enqueue(current_child);
                    }
                }
            }

            return path_to_solution;
        }
    }
}
