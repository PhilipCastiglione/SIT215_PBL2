using System;

namespace SIT221_PBL2
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new Board().FirstNode(4);

            // This might be cool, but it's too slow and never completes
            var search = new DepthFirstSearch(board);

            search.PerformSearch();

            var path = search.SuccessPath();

            Console.WriteLine("Complete");
            Console.ReadLine();
        }
    }
}
