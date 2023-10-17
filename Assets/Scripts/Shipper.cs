using System.Collections.Generic;
using UnityEngine;

public class Shipper : MonoBehaviour
{
    public Node[] nodesList;
    public Node targetPoint;
    public Node previousPoint;
    public Stack<Node> path;
    public Stack<Node> secondPath;
    public LinkManager linkManager;
    public float speed;
    public bool isTakenFood;
    public Node orderPosition;

    public int edgeID;

    private void Awake()
    {
        path = new Stack<Node>();
        nodesList = FindObjectsOfType<Node>();
        targetPoint = null;
        previousPoint = null;
        speed = 30;
        isTakenFood = false;
    }

    private void Update()
    {
        if (targetPoint == null)
            return;
        FindTargetPoint();
        if (path.Count == 0 && Vector3.Distance(transform.position, targetPoint.transform.position) <= 2)
            return;
        FollowTargetPoint();
    }

    private void FollowTargetPoint()
    {
        Vector3 direction = (targetPoint.transform.position - transform.position).normalized;
        transform.LookAt(targetPoint.transform);
        speed = (5 - Astar.trafficLevelList[edgeID]) * 8;
        transform.position += direction * Time.deltaTime * speed;
    }

    private void FindTargetPoint()
    {
        if (path.Count > 0 && Vector3.Distance(transform.position, targetPoint.transform.position) <= 2)
        {
            previousPoint = targetPoint;
            targetPoint = path.Pop();
            if (previousPoint != null)
            {
                foreach (var edge in linkManager.linksList)
                {
                    if ((edge.NodeA == previousPoint && edge.NodeB == targetPoint)
                        || (edge.NodeB == previousPoint && edge.NodeA == targetPoint))
                    {
                        edgeID = edge.edgeID;
                    }
                }
            }
            if (targetPoint == orderPosition)
            {
                edgeID = orderPosition.edgeID;
                return;
            }
            if (path.Count == 0)
            {
                if (secondPath.Count > 0)
                {
                    path = secondPath;
                    isTakenFood = true;
                }
                else
                {
                    path.Push(orderPosition);
                }
            }
        }
    }

    public void FindPath(Node startNode, Node storeNode, Node endNode, Node orderPosition)
    {
        path = Astar.FindPath(startNode, storeNode);
        targetPoint = path.Pop();
        secondPath = Astar.FindPath(storeNode, endNode);
        this.orderPosition = orderPosition;
    }

    public Node FindNearestNode(Vector3 position)
    {
        Node nearestNode = nodesList[0];
        foreach (var node in nodesList)
        {
            if (Vector3.Distance(position, node.transform.position) < Vector3.Distance(position, nearestNode.transform.position))
            {
                nearestNode = node;
            }
        }
        return nearestNode;
    }
}
