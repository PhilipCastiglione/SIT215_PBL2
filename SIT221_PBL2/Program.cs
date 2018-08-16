using System;

namespace SIT221_PBL2
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new Board().FirstNode(5);

            // This might be cool, but it's too slow and never completes
            //var search = new DepthFirstSearch(board);
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

// TODO: comments/docs