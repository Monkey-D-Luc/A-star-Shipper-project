using System.Collections.Generic;
using UnityEngine;

public static class Astar
{
    public static List<int> trafficLevelList = new List<int>();

    public static Stack<Node> FindPath(Node startPoint, Node endPoint)
    {
        Node currentNode; //node hien tai
        Stack<Node> path = new Stack<Node>(); // stack de luu cac node tren duong di tu endPoint den startPoint

        /*
         * danh sach tu sap sep cac phan tu theo thu tu tang dan su dung bien kieu float de so sanh
         * openlist - tap bien
         */
        SortedList<float, Node> openList = new SortedList<float, Node>();
        
        List<Node> closeList = new List<Node>(); //Closelist - tap cac nut duoc mo rong
        openList.Add(0, startPoint); // them startPoint vao openlist
        while (openList.Count > 0)
        {
            currentNode = openList.Values[0]; // lay node gia tri ham F nho nhat trong openlist ra de xet (phan tu dau tien luon co gia tri F nho nhat)
            openList.RemoveAt(0); // RemoveAt(0): loai bo phan tu dau tien ra khoi danh sach
            closeList.Add(currentNode); // them node duoc xet vao closelist
            if (currentNode == endPoint) // neu node hien tai la endPoint thi suy nguoc duong di tu endPoint den startPoint va tra ve danh sach luu cac node tren duong di
            {
                //maxLoop de tranh vong lap vo han 
                int maxLoop = 20;
                do
                {
                    path.Push(currentNode); // day node hien tai vao danh sach duong di
                    currentNode = currentNode.previousNode; // gan node hien tai bang node truoc do
                    maxLoop--;
                } while (currentNode != startPoint && maxLoop > 0); // ket thuc vong lap khi node hien tai la startPoint 
                path.Push(startPoint); // vi startPoint chua duoc day vao danh sach duong di nen ta day startPoint vao danh sach
                return path; // tra ve danh sach va ket thuc
            }

            // vong lap foreach: search "foreach C#" de tim hieu
            foreach (var link in currentNode.neighbours) //xet cac hang xom cua node hien tai (cac duong co the di tu node hien tai)
            {
                /*
                 * neu node hang xom da co trong openlist hoac closelist thi bo qua
                 * link.targetNode = node hang xom dang xet
                 */
                if (openList.ContainsValue(link.targetNode) || closeList.Contains(link.targetNode))
                {
                    continue;
                }

                link.targetNode.previousNode = currentNode; // node truoc cua node hang xom chinh la node hien tai

                /* 
                 * ham G = quang duong shipper phai di tu startPoint den node dang xet
                 *       = G cua node truoc node dang xet + (do dai doan duong giua node truoc va node dang xet * trafficLevel cua doan duong do)
                 */
                link.targetNode.g = currentNode.g + Vector3.Distance(currentNode.transform.position, link.targetNode.transform.position) * trafficLevelList[link.edgeID]; 
                link.targetNode.h = Vector3.Distance(link.targetNode.transform.position, endPoint.transform.position); // do dai duong chim bay giua node dang xet va endPoint
                link.targetNode.f = link.targetNode.g + link.targetNode.h; // ham F = G + H

                /*
                 * push node dang xet vao openlist, su dung gia tri ham F cua node do lam gia tri so sanh
                 * openlist se tu sap xep vi tri cac phan tu theo gia tri cua ham F tang dan
                 */
                openList.Add(link.targetNode.f, link.targetNode); 
            }
        }

        /* 
         * khong con phan tu nao trong openlist de xet ma van chua tim duoc endPoint
         * => khong tim thay duong di => tra ve null
         */
        return null; 
    }
}
