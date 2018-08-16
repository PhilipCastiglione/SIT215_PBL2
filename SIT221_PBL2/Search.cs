using System.Collections.Generic;

namespace SIT221_PBL2
{
    public abstract class Search
    {
        public INode SuccessNode;
        protected Stack<INode> Frontier;
        protected Stack<INode> ExploredNodes;

        public Search(INode firstNode)
        {
            ExploredNodes = new Stack<INode>();
            Frontier = new Stack<INode>();
            Frontier.Push(firstNode);
            SuccessNode = null;
        }

        public abstract void PerformSearch();

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
