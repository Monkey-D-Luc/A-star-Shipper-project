using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    private float counter;
    private float durationToHaveNewOrder;
    public LinkManager linkManager;

    private void Start()
    {
        counter = 0;
        durationToHaveNewOrder = 5;
    }
    private void Update()
    {
        counter += Time.deltaTime;
        if (counter >= durationToHaveNewOrder)
        {
            counter = 0;
            var orderer = ObjectsPooling.Instance.GetPooledObject();
            if (orderer == null)
                return;
            int randomNumber = Random.Range(0, linkManager.roadPositionsList.Count);
            Vector3 randomPos = linkManager.roadPositionsList[randomNumber];
            var nearestNodeA = linkManager.nearestNodesList[randomNumber].Item1;
            var nearestNodeB = linkManager.nearestNodesList[randomNumber].Item2;
            var ordererNode = orderer.GetComponent<Orderer>();
            ordererNode.edgeID = linkManager.nearestNodesList[randomNumber].Item3;
            if (Vector3.Distance(orderer.transform.position, nearestNodeA.transform.position) < Vector3.Distance(orderer.transform.position, nearestNodeB.transform.position))
            {
                ordererNode.nearestNode = nearestNodeA;
            }
            else
            {
                ordererNode.nearestNode = nearestNodeB;
            }
            orderer.transform.position = randomPos;
            orderer.SetActive(true);
        }
    }
}
