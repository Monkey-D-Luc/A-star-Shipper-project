using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node previousNode = null;
    public List<Link> neighbours = new List<Link>();
    public int edgeID = 1;
    public float g = 0;
    public float h;
    public float f;
}
