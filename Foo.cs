public class Node
{
    // TODO enum for state
    public int[] State;
    public int KIndex;

     //node = new Node(currentNode, tile);
    public Node(Node prevNode, int newKIndex)
    {
    }

    public int TileValue()
    {
    }

}

public class Board
{
     public int Dimensions { get; };

     // Board is m * m
     public Board (int dimensions)
     {
        Dim = dimensions;
     }

     private int First => 0;
     private int Last => Dim * Dim - 1;

     // i is the index to move from
     public int[] BoardTilesFrom(int tileIndex)
     {
         int[] validTiles = new int[8];

         // initialise array with sentinals indicating invalid moves
         for (int i = 0; i < 8; i++)
             validTiles[i] = -1;

         if (tileIndex - 2 * Dim >= First && tileIndex % Dim >= 1)
             validTiles[0] = tileIndex - 2 * Dim - 1;

         if (tileIndex - 2 * Dim >= First && tileIndex % Dim <= Dim - 2)
             validTiles[1] = tileIndex - 2 * Dim + 1;

         if (tileIndex - 1 * Dim >= First && tileIndex % Dim <= Dim - 3)
             validTiles[2] = tileIndex - 1 * Dim + 2;

         if (tileIndex + 1 * Dim <= Dim * Dim - 1 && tileIndex % Dim <= Dim - 3)
             validTiles[3] = tileIndex + 1 * Dim + 2;

         if (tileIndex + 2 * Dim <= Dim * Dim - 1 && tileIndex % Dim <= Dim - 2)
             validTiles[4] = tileIndex + 2 * Dim + 1;

         if (tileIndex + 2 * Dim <= Dim * Dim - 1 && tileIndex % Dim >= 1)
             validTiles[5] = tileIndex + 2 * Dim - 1;

         if (tileIndex + 1 * Dim <= Dim * Dim - 1 && tileIndex % Dim >= 2)
             validTiles[6] = tileIndex + 1 * Dim - 2;

         if (tileIndex - 1 * Dim >= First && tileIndex % Dim >= 2)
             validTiles[7] = tileIndex - 1 * Dim - 2;

         return validTiles;
     }

     public void Bar()
     {
         Node[] newNodes = new Node[8];

         int[] validTiles = BoardTilesFrom(currentKnightIndex, boardDim);

         int tile;
         Node node;
         for (int i = 0; i < 8; i++)
         {
             tile = validTiles[i];
             if (tile != -1)
             {
                 // make a new board, check it's value; if it's not V and not T then we will want to add it to the fringe
                 node = new Node(currentNode, tile);
             }
         }
     }
}
