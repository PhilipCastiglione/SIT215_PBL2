using System.Linq;

namespace SIT221_PBL2
{
    class WarnsdorfsRule : Search
    {
        public WarnsdorfsRule(INode firstNode) : base(firstNode) { }

        // IMPLEMENTATION NOTES
        // This implementation is incomplete unless the heuristic is ~"admissible", we only push the most promising
        // of the just found nodes
        //
        // It's also not efficient, we over compute all the next nodes just to count them
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

                // we get the next nodes for our node
                INode[] nextNodes = node.NextNodes();

                // the best next node will be the one with the fewest valid moves from it
                INode bestNextNode = null;
                int bestNextNodeMoves = 9;

                int nextNodeMoves;
                INode nextNode;
                for (int i = 0; i < nextNodes.Length; i++)
                {
                    nextNode = nextNodes[i];
                    if (nextNode.IsTarget())
                    {
                        bestNextNode = nextNode;
                        break;
                    }
                    nextNodeMoves = nextNode.NextNodes().Where(n => !ExploredNodes.Contains(n)).Count();
                    if (nextNodeMoves > 0 && nextNodeMoves < bestNextNodeMoves)
                    {
                        bestNextNodeMoves = nextNodeMoves;
                        bestNextNode = nextNodes[i];
                    }
                }

                if (bestNextNode == null)
                {
                    Frontier.Push(node.Parent);
                }
                else if (!ExploredNodes.Contains(bestNextNode) && !Frontier.Contains(bestNextNode))
                {
                    Frontier.Push(bestNextNode);
                }
            }
        }
    }
}
