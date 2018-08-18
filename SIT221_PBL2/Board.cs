using System;
using System.Linq;

namespace SIT221_PBL2
{
    public class Board : INode
    {
        public enum Cell { Knight, Unvisited, Visited };

        private bool OpenTour;
        private int Dim; // Board is m * m square dimensions
        private int CurrentKnightIndex;
        private int StartIndex;
        public int Visited;
        public Cell[] Cells;

        private const int FirstTileIndex = 0;
        private int LastTileIndex => Dim * Dim - 1;

        private const int MaxPotentialMoves = 8;

        // syntactic sugar
        private Cell this[int index]
        {
            get => Cells[index];
            set => Cells[index] = value;
        }

        public override bool Equals(object obj)
        {
            Board board = obj as Board;
            if (board == null) return false;

            return Enumerable.SequenceEqual(board.Cells, Cells);
        }

        //  this is our first node
        public Board(int dimensions, bool openTour, int startIndex = -1)
        {
            Parent = null;
            Dim = dimensions;
            OpenTour = openTour;
            Visited = 1;

            // start index is set optionally, otherwise pick a middle tile
            StartIndex = (startIndex == -1)? Dim * Dim / 2 : startIndex;
            CurrentKnightIndex = StartIndex;

            Cells = new Cell[Dim * Dim];
            for (int i = FirstTileIndex; i <= LastTileIndex; i++)
                this[i] = Cell.Unvisited;

            this[CurrentKnightIndex] = Cell.Knight;
        }

        // build a next node
        private Board(Board prevBoard, int nextKnightIndex)
        {
            Parent = prevBoard;
            Dim = prevBoard.Dim;
            OpenTour = prevBoard.OpenTour;
            Visited = prevBoard.Visited + 1;
            CurrentKnightIndex = nextKnightIndex;
            StartIndex = prevBoard.StartIndex;

            Cells = new Cell[Dim * Dim];
            for (int i = FirstTileIndex; i <= LastTileIndex; i++)
                this[i] = prevBoard[i];

            this[prevBoard.CurrentKnightIndex] = Cell.Visited;
            this[CurrentKnightIndex] = Cell.Knight;
        }

        public INode Parent { get; set; }

        public INode[] NextNodes()
        {
            // the new nodes indices are those we can reach that are also valid
            int[] newNodeIndices = ValidTileIndices(ReachableTileIndices(CurrentKnightIndex));

            int numberOfNewNodes = newNodeIndices.Where(i => i != -1).Count();
            INode[] nextNodes = new INode[numberOfNewNodes];
            int nextNodeIndex = 0;

            // build the actual next nodes and return a dense array
            for (int i = 0; i < MaxPotentialMoves; i++)
            {
                if (newNodeIndices[i] != -1)
                    nextNodes[nextNodeIndex++] = new Board(this, newNodeIndices[i]);
            }

            return nextNodes;
        }

        public bool IsTarget()
        {
            bool completedTour = Visited == Dim * Dim;

            // an open tour only requires that we have finished a tour, a closed tour requires that
            // we can reach our start index from this position as well
            return OpenTour && completedTour || completedTour && ReachableTileIndices(CurrentKnightIndex).Contains(StartIndex);
        }

        private int[] ReachableTileIndices(int index)
        {
            // initialise array with sentinals indicating invalid moves
            int[] reachableTileIndices = new int[MaxPotentialMoves];
            for (int i = 0; i < MaxPotentialMoves; i++)
                reachableTileIndices[i] = -1;

            // _
            //  |
            //  |
            if (index - 2 * Dim >= FirstTileIndex && index % Dim >= 1)
                reachableTileIndices[0] = index - 2 * Dim - 1;
            //   _
            //  |
            //  |
            if (index - 2 * Dim >= FirstTileIndex && index % Dim <= Dim - 2)
                reachableTileIndices[1] = index - 2 * Dim + 1;
            //
            // ___|
            //
            if (index - 1 * Dim >= FirstTileIndex && index % Dim <= Dim - 3)
                reachableTileIndices[2] = index - 1 * Dim + 2;
            //
            // ___
            //    |
            if (index + 1 * Dim <= Dim * Dim - 1 && index % Dim <= Dim - 3)
                reachableTileIndices[3] = index + 1 * Dim + 2;
            //
            //  |
            //  |_
            if (index + 2 * Dim <= Dim * Dim - 1 && index % Dim <= Dim - 2)
                reachableTileIndices[4] = index + 2 * Dim + 1;
            //
            //  |
            // _|
            if (index + 2 * Dim <= Dim * Dim - 1 && index % Dim >= 1)
                reachableTileIndices[5] = index + 2 * Dim - 1;
            //
            //  ___
            // |
            if (index + 1 * Dim <= Dim * Dim - 1 && index % Dim >= 2)
                reachableTileIndices[6] = index + 1 * Dim - 2;
            //
            // |___
            //
            if (index - 1 * Dim >= FirstTileIndex && index % Dim >= 2)
                reachableTileIndices[7] = index - 1 * Dim - 2;

            return reachableTileIndices;
        }

        private int[] ValidTileIndices(int[] tileIndices)
        {
            for (int i = 0; i < MaxPotentialMoves; i++)
            {
                if (tileIndices[i] != -1)
                {
                    // it is not valid to revisit a tile we have been to already
                    if (Cells[tileIndices[i]] == Cell.Visited)
                        tileIndices[i] = -1;
                }
            }

            return tileIndices;
        }

        public override string ToString()
        {
            string output = "";
            for (int i = FirstTileIndex; i <= LastTileIndex; i++)
            {
                if (i > 0 && i % Dim == 0)
                    output += "\n";
                switch (this[i])
                {
                    case Cell.Knight:
                        output += "K";
                        break;
                    case Cell.Unvisited:
                        output += "U";
                        break;
                    case Cell.Visited:
                        output += "V";
                        break;
                }
            }
            return output;
        }
    }
}
