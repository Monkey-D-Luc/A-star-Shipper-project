using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LinkManager : MonoBehaviour
{
    public GameObject road;
    public GameObject road2;
    public List<Edge> linksList;
    public List<Vector3> roadPositionsList;
    public List<Tuple<Node, Node, int>> nearestNodesList { get; set; }

    private void Awake()
    {
        nearestNodesList = new List<Tuple<Node, Node, int>>();
        Astar.trafficLevelList.Clear();
        int ID = 0;
        foreach (var link in linksList)
        {
            link.edgeID = ID;
            int trafficLevel = Random.Range(1, 5);
            Astar.trafficLevelList.Add(trafficLevel);
            Link2Node(link.NodeA, link.NodeB, ID);
            BuildRoads(link.NodeA, link.NodeB, ID);
            ID++;
        }
    }

    private void BuildRoads(Node nodeA, Node nodeB, int ID)
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
            GameObject roadInstance;
            if (i % 2 == 0)
            {
                roadInstance = Instantiate(road, position, Quaternion.identity);
            }
            else
            {
                roadInstance = Instantiate(road2, position, Quaternion.identity);
            }
            roadInstance.GetComponent<Road>().edgeID = ID;
            roadInstance.transform.LookAt(nodeB.transform);
            roadPositionsList.Add(roadInstance.transform.position);
            nearestNodesList.Add(new Tuple<Node, Node, int>(nodeA, nodeB, ID));
        }
        //if (distance2Road % distance2Road == 0)
        //    return;
        //var lastInstance = Instantiate(road, posB, Quaternion.identity);
        //lastInstance.transform.LookAt(nodeA.transform);
        //roadPositionsList.Add(lastInstance.transform.position);
        //nearestNodesList.Add(new Tuple<Node, Node, int>(nodeA, nodeB, ID));
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
