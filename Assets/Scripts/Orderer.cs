using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orderer : Node
{
    private float counter;
    public Food foodType;
    public Node nearestNode;
    public GameObject shipper;
    public int egdeID;
    public int invoiceCode;
    private void Start()
    {
        counter = 20;
        shipper = null;
    }
    private void Update()
    {
        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == this.shipper)
        {
            var shipper = other.gameObject.GetComponent<Shipper>();
            if (!shipper.isTakenFood)
                return;
            shipper.isTakenFood = false;
            shipper.orderPosition = null;
            shipper.targetPoint = nearestNode;
            shipper.path.Clear();
            gameObject.SetActive(false);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject == this.shipper)
    //    {
    //        var shipper = collision.gameObject.GetComponent<Shipper>();
    //        if (!shipper.isTakenFood)
    //            return;
    //        shipper.isTakenFood = false;
    //        gameObject.SetActive(false);
    //    }
    //}

    private void OnEnable()
    {
        foodType = (Food)Random.Range(0, 2);
        counter = 20;
        shipper = null;
    }
}
public enum Food
{
    Pizza,
    BanhMi
}
