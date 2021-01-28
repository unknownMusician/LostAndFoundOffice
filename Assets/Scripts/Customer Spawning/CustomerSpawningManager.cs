using System.Collections;
using UnityEngine;

namespace CustomerSpawning
{
    public class CustomerSpawningManager : MonoBehaviour
    {
        #region Properties

        private CustomerPoolingManager customerPooling;

        private bool allowCustomerSpawning;

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
            StartCoroutine(TESTTakeCustomerAfterFixedTime(1f));
        }

        #region Methods

        public void StartSpawning()
        {
            // Начинаем спавнить клиентов друг за другом

            allowCustomerSpawning = true;
            TakeCustomer();
        }
        public void StopSpawning()
        {
            // Заканчиваем спавнить клиентов друг за другом

            allowCustomerSpawning = false;
        }

        public void TakeCustomer()
        {
            // Если какой-то клиент уже был заспавлен - пакуем его в переменную oldCustomer
            // Если можно спавнить клиентов - достаем клиента из пула, активируем и настраиваем его случайным образом

            if (allowCustomerSpawning)
            {
                if(newCustomer != null) oldCustomer = newCustomer;

                newCustomer = customerPooling.Pool.Dequeue();
                newCustomer.SetActive(true);
                newCustomer.GetComponent<Customer>().ConfigureProperties();
            }
        }
        private void TakeCustomer(int objectToFindID, float timeToSearch, Sprite image)
        {
            // Если какой-то клиент уже был заспавлен - пакуем его в переменную oldCustomer
            // Если можно спавнить клиентов - достаем клиента из пула, активируем и настраиваем его

            if(allowCustomerSpawning)
            {
                if(newCustomer != null) oldCustomer = newCustomer;

                newCustomer = customerPooling.Pool.Dequeue();
                newCustomer.SetActive(true);
                newCustomer.GetComponent<Customer>().ConfigureProperties(objectToFindID, timeToSearch, image);
            }
        }
        public void PutCustomer()
        {
            // Ложим уходящего клиента обратно в пул

            customerPooling.Pool.Enqueue(oldCustomer);
            oldCustomer.SetActive(false);
        }

        private IEnumerator TESTTakeCustomerAfterFixedTime(float time)
        {
            // Тестовый метод, который нужен только для проверки
            // Выполняем функцкию TakeCustomer() после заданного промежутка времени

            yield return new WaitForSeconds(time);
            TakeCustomer();
        }
    
        #endregion
    }
}
