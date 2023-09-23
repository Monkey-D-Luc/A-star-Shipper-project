using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public static class Astar
{
    public static List<int> trafficLevel = new List<int>() { 1 };

    public static Stack<Node> FindPath(Node startPoint, Node endPoint)
    {
        Node currentNode;
        Stack<Node> path = new Stack<Node>();
        SortedList<float, Node> openList = new SortedList<float, Node>();
        List<Node> closeList = new List<Node>();
        openList.Add(0, startPoint);
        while (openList.Count > 0)
        {
            currentNode = openList.Values[0];
            openList.RemoveAt(0);
            closeList.Add(currentNode);
            if (currentNode == endPoint)
            {
                //maxLoop de tranh vong lap vo han 
                int maxLoop = 20;
                do
                {
                    path.Push(currentNode);
                    currentNode = currentNode.previousNode;
                    maxLoop--;
                } while (currentNode != startPoint && maxLoop > 0);
                path.Push(startPoint);
                return path;
            }
            foreach (var link in currentNode.neighbours)
            {
                if (openList.ContainsValue(link.targetNode) || closeList.Contains(link.targetNode))
                {
                    continue;
                }
                link.targetNode.previousNode = currentNode;
                currentNode.edgeID = trafficLevel[0];
                link.targetNode.g = currentNode.g + Vector3.Distance(currentNode.transform.position, link.targetNode.transform.position) * trafficLevel[0];
                link.targetNode.h = Vector3.Distance(link.targetNode.transform.position, endPoint.transform.position);
                link.targetNode.f = link.targetNode.g + link.targetNode.h;
                openList.Add(link.targetNode.f, link.targetNode);
            }
        }

        return null;
    }
}
