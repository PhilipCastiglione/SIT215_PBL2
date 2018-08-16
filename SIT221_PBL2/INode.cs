namespace SIT221_PBL2
{
    public interface INode
    {
        INode Parent { get; set; }

        INode FirstNode(object state);

        INode[] NextNodes();

        bool IsTarget();
    }
}
