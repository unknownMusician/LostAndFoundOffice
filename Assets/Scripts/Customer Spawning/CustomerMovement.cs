using System.Collections;
using UnityEngine;


namespace CustomerSpawning
{
    public class CustomerMovement : MonoBehaviour
    {
        #region Properties

        private Rigidbody rigidbodyComponent;
        private Customer customerComponent;

        private CustomerSpawningManager customerSpawner;
        private AllCustomersMovingManager allCustomerMovingManager;

        private int IdOfCurrentRoute;                       // ID текущего маршрута (0 - вход, 1 - выход)
        private int IdOfCurrentGoalPoint;                   // ID текущей целевой точки в маршруте
        private Vector3 moveVector;                         // Вектор, направляющий customer'a от одной точки к другой

        #endregion

        #region Behaviour methods

        void Awake()
        {
            rigidbodyComponent = GetComponent<Rigidbody>();
            customerComponent = GetComponent<Customer>();

            customerSpawner = transform.parent.parent.GetComponent<CustomerSpawningManager>();
            allCustomerMovingManager = transform.parent.parent.GetComponent<AllCustomersMovingManager>();
        }
        void OnEnable()
        {
            // Запрещаем двигаться. Устанавливаем текущий маршрут. Задаём начальную позицию
            
            transform.position = allCustomerMovingManager.Routes[0][0];
            EnterBuilding();
        }

        #endregion

        #region Methods

        public void EnterBuilding()
        {
            StartCoroutine(EnterBuildingCoroutine());
        }
        public void ExitBuilding()
        {
            StartCoroutine(ExitBuildingCoroutine());
        }

        public bool CheckIfCurrentGoalPointIsLast()
        {
            // Проверяем, является ли текущая целевая точка последней

            return IdOfCurrentGoalPoint + 1 >= allCustomerMovingManager.Routes[IdOfCurrentRoute].Length;
        }
        private void CalculateMoveVector()
        {
            // Рассчитываем направляющий вектор

            if (IdOfCurrentGoalPoint != 0)
            {
                moveVector = allCustomerMovingManager.Routes[IdOfCurrentRoute][IdOfCurrentGoalPoint] - allCustomerMovingManager.Routes[IdOfCurrentRoute][IdOfCurrentGoalPoint - 1];
                NormalizeMoveVector();
            }
        }
        private void NormalizeMoveVector()
        {
            // Нормализуем направляющий вектор, чтобы его координаты не были больше 1

            if (Mathf.Abs(moveVector.x) >= Mathf.Abs(moveVector.z))
            {
                float normalizer = Mathf.Abs(moveVector.x);
                moveVector = new Vector3(moveVector.x / normalizer, moveVector.y / normalizer, moveVector.z / normalizer);
            }
            else
            {
                float normalizer = Mathf.Abs(moveVector.z);
                moveVector = new Vector3(moveVector.x / normalizer, moveVector.y / normalizer, moveVector.z / normalizer);
            }
        }

        private IEnumerator EnterBuildingCoroutine()
        {
            // Запускаем процесс входа в здание

            IdOfCurrentRoute = 0;
            transform.position = allCustomerMovingManager.enterRoute[0];

            for (int i = 1; i < allCustomerMovingManager.enterRoute.Length; i++)
            {
                yield return null;
                IdOfCurrentGoalPoint = i;
                Vector3 deltaVector = transform.position - allCustomerMovingManager.Routes[IdOfCurrentRoute][i];
                float vectorLength = deltaVector.magnitude;
                CalculateMoveVector();
                while (vectorLength > 0.5f)
                {
                    yield return new WaitForSeconds(0.00033f);
                    deltaVector = transform.position - allCustomerMovingManager.Routes[IdOfCurrentRoute][i];
                    vectorLength = deltaVector.magnitude;
                    rigidbodyComponent.velocity = moveVector * allCustomerMovingManager.speed;
                }
                rigidbodyComponent.velocity = Vector3.zero;
            }

            customerComponent.AllowRotation = false;
            customerComponent.MakeAnOrder();
        }
        private IEnumerator ExitBuildingCoroutine()
        {
            // Запускаем процесс выхода из здания

            IdOfCurrentRoute = 1;
            customerSpawner.TakeCustomer();
            customerComponent.AllowRotation = true;
            transform.position = allCustomerMovingManager.exitRoute[0];

            for (int i = 1; i < allCustomerMovingManager.exitRoute.Length; i++)
            {
                yield return null;
                IdOfCurrentGoalPoint = i;
                Vector3 deltaVector = transform.position - allCustomerMovingManager.Routes[IdOfCurrentRoute][i];
                float vectorLength = deltaVector.magnitude;
                CalculateMoveVector();
                while (vectorLength > 1.5f)
                {
                    yield return new WaitForSeconds(0.00033f);
                    deltaVector = transform.position - allCustomerMovingManager.Routes[IdOfCurrentRoute][i];
                    vectorLength = deltaVector.magnitude;
                    rigidbodyComponent.velocity = moveVector * allCustomerMovingManager.speed;
                }
                rigidbodyComponent.velocity = Vector3.zero;
            }
            if (transform.childCount == 2) transform.GetChild(1).GetComponent<Interaction.Item>().EndItem();
            customerSpawner.PutCustomer();
        }

        #endregion
    }
}
