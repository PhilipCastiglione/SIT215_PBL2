using System;
using System.Linq;

namespace SIT221_PBL2
{
    public class Board : INode
    {
        public enum Cell { Knight, Unvisited, Visited };

        private int Dim; // Board is m * m square dimensions
        private int CurrentKnightIndex;
        private int StartIndex;
        private int Visited;
        public Cell[] Cells;

        private const int FirstTileIndex = 0;
        private int LastTileIndex => Dim * Dim - 1;

        private const int MaxPotentialMoves = 8;

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

        public Board() {}

        private Board(int dimensions)
        {
            Parent = null;
            Dim = dimensions;
            Visited = 1;
            CurrentKnightIndex = 0; // TODO: does starting location matter?
            StartIndex = 0; // TODO: same as current knight index here

            Cells = new Cell[Dim * Dim];
            for (int i = FirstTileIndex; i <= LastTileIndex; i++)
                this[i] = Cell.Unvisited;

            this[CurrentKnightIndex] = Cell.Knight;
        }

        private Board(Board prevBoard, int nextKnightIndex)
        {
            Parent = prevBoard;
            Dim = prevBoard.Dim;
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

        public INode FirstNode(object dim)
        {
            return new Board((int)dim);
        }

        public INode[] NextNodes()
        {
            int[] newNodeIndices = ValidTileIndices(ReachableTileIndices());

            int numberOfNewNodes = newNodeIndices.Where(i => i != -1).Count();
            INode[] nextNodes = new INode[numberOfNewNodes];
            int nextNodeIndex = 0;

            for (int i = 0; i < MaxPotentialMoves; i++)
            {
                if (newNodeIndices[i] != -1)
                    nextNodes[nextNodeIndex++] = new Board(this, newNodeIndices[i]);
            }

            return nextNodes;
        }

        public bool IsTarget()
        {
            // TODO: closed/open? currently this is open
            return Visited == 64;
        }


        private int[] ReachableTileIndices()
        {
            // initialise array with sentinals indicating invalid moves
            int[] reachableTileIndices = new int[8];
            for (int i = 0; i < MaxPotentialMoves; i++)
                reachableTileIndices[i] = -1;

            // _
            //  |
            //  |
            if (CurrentKnightIndex - 2 * Dim >= FirstTileIndex && CurrentKnightIndex % Dim >= 1)
                reachableTileIndices[0] = CurrentKnightIndex - 2 * Dim - 1;
            //   _
            //  |
            //  |
            if (CurrentKnightIndex - 2 * Dim >= FirstTileIndex && CurrentKnightIndex % Dim <= Dim - 2)
                reachableTileIndices[1] = CurrentKnightIndex - 2 * Dim + 1;
            //
            // ___|
            //
            if (CurrentKnightIndex - 1 * Dim >= FirstTileIndex && CurrentKnightIndex % Dim <= Dim - 3)
                reachableTileIndices[2] = CurrentKnightIndex - 1 * Dim + 2;
            //
            // ___
            //    |
            if (CurrentKnightIndex + 1 * Dim <= Dim * Dim - 1 && CurrentKnightIndex % Dim <= Dim - 3)
                reachableTileIndices[3] = CurrentKnightIndex + 1 * Dim + 2;
            //
            //  |
            //  |_
            if (CurrentKnightIndex + 2 * Dim <= Dim * Dim - 1 && CurrentKnightIndex % Dim <= Dim - 2)
                reachableTileIndices[4] = CurrentKnightIndex + 2 * Dim + 1;
            //
            //  |
            // _|
            if (CurrentKnightIndex + 2 * Dim <= Dim * Dim - 1 && CurrentKnightIndex % Dim >= 1)
                reachableTileIndices[5] = CurrentKnightIndex + 2 * Dim - 1;
            //
            //  ___
            // |
            if (CurrentKnightIndex + 1 * Dim <= Dim * Dim - 1 && CurrentKnightIndex % Dim >= 2)
                reachableTileIndices[6] = CurrentKnightIndex + 1 * Dim - 2;
            //
            // |___
            //
            if (CurrentKnightIndex - 1 * Dim >= FirstTileIndex && CurrentKnightIndex % Dim >= 2)
                reachableTileIndices[7] = CurrentKnightIndex - 1 * Dim - 2;

            return reachableTileIndices;
        }

        private int[] ValidTileIndices(int[] tileIndices)
        {
            Func<int, bool> tileIsReachable, cellIsVisited, cellIsFinalMove;
            for (int i = 0; i < MaxPotentialMoves; i++)
            {
                tileIsReachable = t => tileIndices[t] != -1;
                cellIsVisited = t => Cells[tileIndices[t]] == Cell.Visited;
                cellIsFinalMove = t => tileIndices[t] == StartIndex && Visited == 63;
                if (tileIsReachable(i) && cellIsVisited(i) && !cellIsFinalMove(i))
                    tileIndices[i] = -1;
            }

            return tileIndices;
        }
    }
}
