using System;

namespace SIT221_PBL2
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new Board().FirstNode(4);

            var search = new DepthFirstSearch(board);

            search.PerformSearch();

            // TODO: there is a bug, because it doesn't succeed yet.
            var path = search.SuccessPath();

            Console.WriteLine("Complete");
        }
    }
}
