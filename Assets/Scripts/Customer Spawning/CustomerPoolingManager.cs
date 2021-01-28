using System.Collections.Generic;
using UnityEngine;


namespace CustomerSpawning
{
    public class CustomerPoolingManager : MonoBehaviour
    {
        #region Properties

        private AllCustomersMovingManager allCustomersMovingManager;

        public Transform customersArrayMenu;
        public GameObject customerPrefab;
        public int allowedAmountOfCustomers;

        public Queue<GameObject> Pool { get; set; }

        #endregion

        void Awake()
        {
            allCustomersMovingManager = GetComponent<AllCustomersMovingManager>();
            Pool = new Queue<GameObject>();
        }
        void Start()
        {
            CreatePool();
        }

        private void CreatePool()
        {
            // Создаём пул клиентов
            // Располагаем их на нулевой точке маршрута ВХОДА в здание
            // Отключаем их
            // Добавляем в очередь

            Vector3 customersStartPosition = allCustomersMovingManager.Routes[0][0];

            for (int i = 0; i < allowedAmountOfCustomers; i++)
            {
                GameObject customer = Instantiate(
                    customerPrefab,
                    customersStartPosition,
                    Quaternion.identity,
                    customersArrayMenu
                    );
                customer.SetActive(false);
                Pool.Enqueue(customer);
            }
        }
    }
}
