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

        public Board(int dimensions, bool openTour)
        {
            Parent = null;
            Dim = dimensions;
            OpenTour = openTour;
            Visited = 1;
            // TODO: implement starting location pass through
            // for now, just start in the middle-ish of the board
            CurrentKnightIndex = Dim * Dim / 2 + Dim / 2;
            StartIndex = CurrentKnightIndex;
            Cells = new Cell[Dim * Dim];
            for (int i = FirstTileIndex; i <= LastTileIndex; i++)
                this[i] = Cell.Unvisited;

            this[CurrentKnightIndex] = Cell.Knight;
        }

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
            bool completedTour = Visited == Dim * Dim;

            return OpenTour && completedTour || completedTour && ReachableTileIndices().Contains(StartIndex);
        }


        private int[] ReachableTileIndices()
        {
            // initialise array with sentinals indicating invalid moves
            int[] reachableTileIndices = new int[MaxPotentialMoves];
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
            for (int i = 0; i < MaxPotentialMoves; i++)
            {
                if (tileIndices[i] != -1 && Cells[tileIndices[i]] == Cell.Visited)
                    tileIndices[i] = -1;
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
