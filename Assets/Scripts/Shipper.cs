using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Shipper : MonoBehaviour
{
    public Node[] nodesList;
    public Node targetPoint;
    public Stack<Node> path;
    public Stack<Node> secondPath;
    public LinkManager linkManager;
    public float speed;
    public int invoiceCode;
    public bool isTakenFood;
    public Node orderPosition;

    public int trafficLevel;

    private void Awake()
    {
        path = new Stack<Node>();
        nodesList = FindObjectsOfType<Node>();
        targetPoint = null;
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
    public void FindAndFollowPath(Node startPoint, Node endPoint)
    {
        var startNode = FindNearestNode(startPoint.transform.position);
        var endNode = FindNearestNode(endPoint.transform.position);
        path = Astar.FindPath(startNode, endNode);

        //FollowPath(path);
    }

    private void FollowTargetPoint()
    {
        Vector3 direction = (targetPoint.transform.position - transform.position).normalized;
        transform.LookAt(targetPoint.transform);
        Debug.Log(Astar.trafficLevel[targetPoint.edgeID]);
        transform.position += direction * Time.deltaTime * speed / Astar.trafficLevel[targetPoint.edgeID];
    }

    private void FindTargetPoint()
    {
        if (path.Count > 0 && Vector3.Distance(transform.position, targetPoint.transform.position) <= 2)
        {
            targetPoint = path.Pop();
            if (targetPoint == orderPosition)
                return;
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

    public void FindPath()
    {
        path = Astar.FindPath(linkManager.startPoint, linkManager.endPoint);
        targetPoint = path.Pop();
    }

    public void FindPath(Node startNode, Node storeNode , Node endNode, Node orderPosition)
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
