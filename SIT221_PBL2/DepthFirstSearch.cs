namespace SIT221_PBL2
{
    /**
     * Straightforward DFS implementation. DFS can be used for Knight's Tour on
     * small boards practically. Uninformed search rapidly becomes too slow.
     */
    public class DepthFirstSearch : Search
    {
        public DepthFirstSearch(INode firstNode) : base(firstNode) { }

        public override void PerformSearch()
        {
            while (Frontier.Count > 0)
            {
                INode node = Frontier.Pop();
                ExploredNodes.Push(node);

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
            }
        }
    }
}
