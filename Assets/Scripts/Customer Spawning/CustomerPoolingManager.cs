using System.Collections.Generic;
using UnityEngine;


public class CustomerPoolingManager : MonoBehaviour
{
    #region Properties

    public Transform customersArrayMenu;
    public GameObject customerPrefab;

    private int allowedAmountOfCustomers;
    public Queue<GameObject> Pool { get; set; }

    #endregion

    void Awake()
    {
        allowedAmountOfCustomers = 2;
        Pool = new Queue<GameObject>();
    }
    void Start()
    {
        CreateCustomersPool();
    }

    private void CreateCustomersPool()
    {
        for (int i = 0; i < allowedAmountOfCustomers; i++)
        {
            float xCoord = -10;
            float yCoord = 1;
            float zCoord = 17;
            GameObject customer = Instantiate(
                customerPrefab,
                new Vector3(xCoord, yCoord, zCoord),
                Quaternion.identity,
                customersArrayMenu
                );
            customer.SetActive(false);
            Pool.Enqueue(customer);
        }
    }
}
