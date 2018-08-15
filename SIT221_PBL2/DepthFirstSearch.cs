using System.Collections.Generic;

namespace SIT221_PBL2
{
    public class DepthFirstSearch
    {
        private Stack<INode> ExploredNodes;
        private Stack<INode> Frontier;
        public INode SuccessNode;

        public DepthFirstSearch(INode firstNode)
        {
            ExploredNodes = new Stack<INode>();
            Frontier = new Stack<INode>();
            Frontier.Push(firstNode);
            SuccessNode = null;
        }

        public void PerformSearch()
        {
            while (Frontier.Count > 0)
            {
                INode node = Frontier.Pop();

                if (node.IsTarget())
                {
                    SuccessNode = node;
                    return;
                }

                INode[] nextNodes = node.NextNodes();

                foreach (INode n in nextNodes)
                {
                    if (!ExploredNodes.Contains(n) && !Frontier.Contains(n))
                    {
                        Frontier.Push(n);
                    }
                }
                    
                ExploredNodes.Push(node);
            }
        }

        public Stack<INode> SuccessPath()
        {
            var successPath = new Stack<INode>();

            var currentNode = SuccessNode;

            while (currentNode != null)
            {
                successPath.Push(currentNode);
                currentNode = currentNode.Parent;
            }

            return successPath;
        }
    }
}
