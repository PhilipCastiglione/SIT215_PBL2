using System;

namespace SIT221_PBL2
{
    class Program
    {
        static void Main(string[] args)
        {
            // dimensions should be no less than 5 to find a solution
            // closed tours only exist for some configurations and stating positions
            int dimensions = 5;
            bool openTour = false;
            int startIndex = 0;
            var board = new Board(dimensions, openTour);//, startIndex);

            // Instantiate our search with the first node
            // var search = new DepthFirstSearch(board); // DFS is cool, but too slow for m > 5
            var search = new WarnsdorfsRule(board);

            search.PerformSearch();

            var path = search.SuccessPath();

            Console.WriteLine("Complete");
            if (path.Count == 0)
            {
                Console.WriteLine("Failed to find solution!");
            }
            else
            {
                foreach (INode n in path)
                {
                    Console.WriteLine("---------------");
                    Console.WriteLine(n);
                    Console.WriteLine("---------------");
                }
            }
            Console.ReadLine();
        }
    }
}
