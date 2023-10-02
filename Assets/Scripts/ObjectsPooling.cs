using System.Collections.Generic;
using UnityEngine;

public class ObjectsPooling : MonoBehaviour
{
    public static ObjectsPooling Instance;
    public List<GameObject> pooledObject;
    [SerializeField] GameObject objectToPool;
    [SerializeField] int amount;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        pooledObject = new List<GameObject>();
        GameObject temp;
        for (int i = 0; i < amount; i++)
        {
            temp = Instantiate(objectToPool, this.transform);
            temp.SetActive(false);
            pooledObject.Add(temp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amount; i++)
        {
            if (!pooledObject[i].activeInHierarchy)
            {
                return pooledObject[i];
            }
        }
        return null;
    }
}
