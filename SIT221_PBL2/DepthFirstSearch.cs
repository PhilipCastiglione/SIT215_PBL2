﻿using System.Collections.Generic;

namespace SIT221_PBL2
{
    public class DepthFirstSearch : Search
    {
        public DepthFirstSearch(INode firstNode) : base(firstNode) { }

        public override void PerformSearch()
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
    }
}
