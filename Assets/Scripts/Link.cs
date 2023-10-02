[System.Serializable]
public class Link
{
    public Node beginNode;
    public Node targetNode;
    public int edgeID;

    public Link(Node begin, Node target, int edgeID)
    {
        this.beginNode = begin;
        this.targetNode = target;
        this.edgeID = edgeID;
    }
}
