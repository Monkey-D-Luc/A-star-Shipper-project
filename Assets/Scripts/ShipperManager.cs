using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipperManager : MonoBehaviour
{
    public Transform selectedShipper;
    public Transform selectedOrderer;
    public Material selectedMaterial;
    public Material unselectedMaterial;

    public Node pizzaStore;
    public Node banhMiStore;

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        if (Physics.Raycast(ray, out hitData) && Input.GetKey(KeyCode.Mouse0))
        {
            if (hitData.collider.CompareTag("Shipper"))
            {
                if (selectedShipper != null)
                {
                    selectedShipper.GetComponent<Renderer>().material = unselectedMaterial;
                }
                selectedShipper = hitData.transform;
                selectedShipper.GetComponent<Renderer>().material = selectedMaterial;
            }
            if (hitData.collider.CompareTag("Orderer"))
            {
                if (selectedShipper != null)
                {
                    selectedOrderer = hitData.transform;
                    var shipper = selectedShipper.GetComponent<Shipper>();
                    var orderer = selectedOrderer.GetComponent<Orderer>();
                    //Debug.Log(selectedShipper.position);
                    Node startNode = shipper.FindNearestNode(selectedShipper.position);
                    Node storeNode = null;
                    if (orderer.foodType == Food.Pizza)
                    {
                        storeNode = pizzaStore;
                    }
                    else if (orderer.foodType == Food.BanhMi)
                    {
                        storeNode = banhMiStore;
                    }
                    if (storeNode == null)
                        return;
                    //Debug.Log(startNode);
                    //Debug.Log(storeNode);
                    //Debug.Log(orderer.nearestNode);
                    orderer.shipper = shipper.gameObject;
                    shipper.FindPath(startNode, storeNode, orderer.nearestNode);
                    selectedOrderer = null;
                    selectedShipper = null;
                    Debug.Log("Call");
                }
            }
        }
    }
}
