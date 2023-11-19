using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Magic_Puzzle
{
    public abstract class SearchAlgorithm
    {
        // Abstract method to be implemented by derived classes for searching a path.
        public abstract List<Node> Search(Node root);

        protected void Path_Tracer(List<Node> path, Node n)
        {
            Console.WriteLine("Retracing Path...");

            // Start from the given inital node.
            Node current = n;           
            path.Add(current); // Add it to the path list.

            // Trace back to the root node.
            while (current.parent != null)
            {        
                current = current.parent; // Move to the parent node.               
                path.Add(current); // Add the current node to the path.
            }
            path.Reverse();
        }

        // Checks if the given node is in the list based on a puzzle match.
        protected static bool Contains(List<Node> list, Node c)
        {
            bool contains = false;

            // Loop through all nodes in the list.
            for (int i = 0; i < list.Count; i++)
            {
                // If the puzzle of the list node matches the puzzle of the given node.
                if (list[i].SameP(c.puzzle))
                {
                    contains = true;
                }
            }

            // Return whether a match was found.
            return contains;
        }
    }

}
