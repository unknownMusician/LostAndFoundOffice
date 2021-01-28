using System.Collections;
using UnityEngine;


public class CustomerSpawningManager : MonoBehaviour
{
    #region Properties

    private CustomerPoolingManager customerPooling;

    public bool allowCustomerSpawning;

    private GameObject newCustomer;
    private GameObject oldCustomer;

    #endregion

    void Awake()
    {
        customerPooling = GetComponent<CustomerPoolingManager>();
        allowCustomerSpawning = false;
    }
    void Start()
    {
        allowCustomerSpawning = true;
        StartCoroutine(TakeCustomerFromPoolAfterFixedTime(1f));
    }

    #region Methods

    public void StartCustomerSpawningCycle()
    {
        allowCustomerSpawning = true;
        TakeNewCustomerFromPoolAndConfigureHim();
    }
    public void StopCustomerSpawningCycle()
    {
        allowCustomerSpawning = false;
    }

    public void TakeNewCustomerFromPoolAndConfigureHim()
    {
        if (newCustomer != null)
        {
            oldCustomer = newCustomer;
        }
        if (allowCustomerSpawning)
        {
            newCustomer = customerPooling.Pool.Dequeue();
            newCustomer.SetActive(true);
            newCustomer.GetComponent<Customer>().ConfigureProperties();
        }
    }
    private void TakeNewCustomerFromPoolAndConfigureHim(int objectToFindID, float timeToSearch, Sprite image)
    {
        newCustomer = customerPooling.Pool.Dequeue();
        newCustomer.SetActive(true);
        newCustomer.GetComponent<Customer>().ConfigureProperties(objectToFindID, timeToSearch, image);
    }
    public void PutOldCustomerToPool()
    {
        customerPooling.Pool.Enqueue(oldCustomer);
        oldCustomer.SetActive(false);
    }

    private IEnumerator TakeCustomerFromPoolAfterFixedTime(float time)
    {
        yield return new WaitForSeconds(time);
        TakeNewCustomerFromPoolAndConfigureHim();
    }
    
    #endregion
}
