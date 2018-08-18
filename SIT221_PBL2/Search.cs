using System.Collections.Generic;

namespace SIT221_PBL2
{
    /**
     * Contains common behaviour to search forms. May need refactoring
     * if a search approach is implemented that can't use stacks. Note
     * that this is fine for algorithms usually implemented recursively,
     * we are just unrolling the recursion into a loop and using an
     * explicit stack.
     */
    public abstract class Search
    {
        public INode SuccessNode;
        public Stack<INode> Frontier;
        public Stack<INode> ExploredNodes;

        // set up our search and push the first node onto the stack
        public Search(INode firstNode)
        {
            ExploredNodes = new Stack<INode>();
            Frontier = new Stack<INode>();
            Frontier.Push(firstNode);
            SuccessNode = null;
        }

        public abstract void PerformSearch();

        // return the success path as a stack with the first move on top
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
