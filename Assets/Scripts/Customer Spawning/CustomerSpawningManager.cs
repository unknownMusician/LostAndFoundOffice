using UnityEngine;


namespace CustomerSpawning
{
    public class CustomerSpawningManager : MonoBehaviour
    {
        #region Instance

        public static CustomerSpawningManager instance = null;
        private void Awake() {
            instance = this;
            DataManager.Manager.Fin += StopSpawning;
        }
        private void OnDestroy() => instance = null;

        #endregion

        #region Properties

        private CustomerPoolingManager customerPooling;

        private bool allowCustomerSpawning;

        private GameObject newCustomer;
        private GameObject oldCustomer;

        #endregion

        void OnEnable()
        {
            customerPooling = GetComponent<CustomerPoolingManager>();
            allowCustomerSpawning = false;
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
                if (newCustomer != null) oldCustomer = newCustomer;

                newCustomer = customerPooling.Pool.Dequeue();
                newCustomer.SetActive(true);
            }
        }
        public void PutCustomer()
        {
            // Ложим уходящего клиента обратно в пул

            customerPooling.Pool.Enqueue(oldCustomer);
            oldCustomer.SetActive(false);
        }

        #endregion
    }
}
