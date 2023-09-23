using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LinkManager : MonoBehaviour
{
    public Node startPoint;
    public Node endPoint;
    public GameObject road;
    public List<Edge> linksList;
    public List<Vector3> roadPositionsList;
    public List<Tuple<Node, Node, int>> nearestNodesList;

    private void Awake()
    {
        Debug.Log(IntPtr.Size);
        nearestNodesList = new List<Tuple<Node, Node, int>>();
        int ID = 0;
        foreach (var link in linksList)
        {
            Link2Node(link.NodeA, link.NodeB, ID);
            BuildRoads(link.NodeA, link.NodeB, ID);
            ID++;
        }

        Stack<Node> path = new Stack<Node>();
        path = Astar.FindPath(startPoint, endPoint);
        //while (path.Count > 0)
        //{
        //    Node node = path.Pop();
        //    var nodePos = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //    nodePos.transform.position = node.transform.position;
        //    var renderer = nodePos.GetComponent<Renderer>();
        //    renderer.material.SetColor("_Color", Color.red);
        //    nodePos.transform.localScale = new Vector3(7, 7, 7);
        //}
    }

    private void BuildRoads(Node nodeA, Node nodeB,int ID)
    {
        Vector3 posA = nodeA.transform.position;
        Vector3 posB = nodeB.transform.position;
        float distance2Node = Vector3.Distance(nodeA.transform.position, nodeB.transform.position);
        int distance2Road = 5;
        int nRoad = (int)distance2Node / distance2Road;
        
        for (int i = 0; i < nRoad; i++)
        {
            float n = 1.0f / nRoad * i;
            Vector3 position = Vector3.Lerp(posA, posB, n);
            var roadInstance = Instantiate(road, position, Quaternion.identity);
            roadInstance.transform.LookAt(nodeB.transform);
            roadPositionsList.Add(roadInstance.transform.position);
            nearestNodesList.Add(new Tuple<Node, Node, int>(nodeA, nodeB, ID));
        }
        if (distance2Road % distance2Road == 0)
            return;
        var lastInstance = Instantiate(road, posB, Quaternion.identity);
        lastInstance.transform.LookAt(nodeA.transform);
        roadPositionsList.Add(lastInstance.transform.position);
        nearestNodesList.Add(new Tuple<Node, Node, int>(nodeA, nodeB, ID));
    }

    public void Link2Node(Node nodeA, Node nodeB, int edgeID)
    {
        var linkA = new Link(nodeA, nodeB, edgeID);
        nodeA.neighbours.Add(linkA);
        var linkB = new Link(nodeB, nodeA, edgeID);
        nodeB.neighbours.Add(linkB);
    }

    private void OnDrawGizmos()
    {
        foreach (var link in linksList)
        {
            Gizmos.DrawLine(link.NodeA.transform.position, link.NodeB.transform.position);
        }
    }
}
