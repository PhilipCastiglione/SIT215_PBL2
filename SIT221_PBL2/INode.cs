namespace SIT221_PBL2
{
    /**
     * Implementing INode means that a class can be passed into an implementation of Search.
     */
    public interface INode
    {
        int Depth { get; set; }
        INode Parent { get; set; }

        INode[] NextNodes();

        bool IsLeaf();

        bool IsTarget();
    }
}
