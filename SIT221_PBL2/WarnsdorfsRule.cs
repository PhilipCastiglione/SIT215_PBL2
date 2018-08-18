using System;
using System.Linq;

namespace SIT221_PBL2
{
    /**
     * Implements:
     * - Warnsdorf's Rule: 
     * - with backtracking: 
     * 
     * Note:
     * Not highly efficient as we fully compute future nodes for each current node to count
     * available moves. Empirically this still runs fast for fairly large board sizes.
     */
    class WarnsdorfsRule : Search
    {
        public WarnsdorfsRule(INode firstNode) : base(firstNode) { }

        public override void PerformSearch()
        {
            while (Frontier.Count > 0)
            {
                INode node = Frontier.Pop();
                // we might be back in a parent, don't re-push it into explored
                if (!ExploredNodes.Contains(node))
                    ExploredNodes.Push(node);

                if (node.IsTarget())
                {
                    SuccessNode = node;
                    return;
                }

                // we get the next nodes for our node - given backtracking some of these might have been explored
                INode[] nextNodes = node.NextNodes();

                // the best next node will be the one with the fewest valid, unexplored moves from it
                INode bestNextNode = null;
                int bestNextNodeMoves = int.MaxValue;

                INode nextNode;
                int nextNodeMoves;
                for (int i = 0; i < nextNodes.Length; i++)
                {
                    // for each next node from our current node that we haven't explored, we will look at the number of moves from that node
                    nextNode = nextNodes[i];
                    
                    if (nextNode.IsTarget()) // shortcircuit
                    {
                        SuccessNode = nextNode;
                        return;
                    }

                    // we want the best node we can see out of the next nodes, based on how many moves each has
                    nextNodeMoves = nextNode.NextNodes().Where(n => !ExploredNodes.Contains(n)).Count();
                    if (nextNodeMoves > 0 && nextNodeMoves < bestNextNodeMoves)
                    {
                        bestNextNodeMoves = nextNodeMoves;
                        bestNextNode = nextNode;
                    }
                }

                // backtrack if we didn't find a node to move forward to
                if (bestNextNode == null)
                {
                    Frontier.Push(node.Parent);
                }
                // if we did find a node, place it onto the stack
                else if (!ExploredNodes.Contains(bestNextNode) && !Frontier.Contains(bestNextNode))
                {
                    Frontier.Push(bestNextNode);
                }
            }
        }
    }
}
