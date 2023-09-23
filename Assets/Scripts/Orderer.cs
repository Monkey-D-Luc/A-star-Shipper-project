using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Orderer : Node
{
    private float counter;
    private TextMeshProUGUI infoText;
    public string foodInfo;
    public Food foodType;
    public Node nearestNode;
    public GameObject shipper;
    public int egdeID;
    public int invoiceCode;

    private void Awake()
    {
        infoText = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Start()
    {
        counter = 20;
        shipper = null;
        //foodInfo = "Pizza";
    }
    private void Update()
    {
        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            if (this.shipper!= null)
            {
                var shipper = this.shipper.GetComponent<Shipper>();
                shipper.isTakenFood = false;
                shipper.orderPosition = null;
                shipper.path.Clear();
            }
            gameObject.SetActive(false);
        }
        int timeRemain = (int)counter;
        //infoText.SetText(foodInfo);
        infoText.SetText(foodInfo + " - " + timeRemain.ToString());
        Debug.Log(foodInfo);
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
        switch (foodType)
        {
            case Food.Pizza:
                Debug.Log("Pizza");
                foodInfo = "Pizza"; break;
            case Food.BanhMi:
                Debug.Log("Banh Mi");
                foodInfo = "Banh Mi"; break;
                //default: break;
        }
        //if (foodType == Food.Pizza)
        //{
        //    foodInfo = "Pizza";
        //}
        //else
        //{
        //    foodInfo = "Banh Mi";
        //}
    }
}
public enum Food
{
    Pizza,
    BanhMi
}
