using System.Collections;
using UnityEngine;

namespace CustomerSpawning
{
    public class CustomerMovement : MonoBehaviour
    {
        #region Properties

        private Rigidbody rigidbodyComponent;
        private Transform transformComponent;
        private Customer customerComponent;

        private CustomerSpawningManager customerSpawner;
        private AllCustomersMovingManager allCustomerMovingManager;

        private bool allowMoving;
        private int IdOfCurrentRoute;                       // ID текущего маршрута (0 - вход, 1 - выход)
        private int IdOfCurrentGoalPoint;                   // ID текущей целевой точки в маршруте
        private Vector3 moveVector;                         // Вектор, направляющий customer'a от одной точки к другой

        #endregion

        #region Behaviour methods

        void Awake()
        {
            rigidbodyComponent = GetComponent<Rigidbody>();
            transformComponent = GetComponent<Transform>();
            customerComponent = GetComponent<Customer>();

            customerSpawner = transformComponent.parent.parent.GetComponent<CustomerSpawningManager>();
            allCustomerMovingManager = transformComponent.parent.parent.GetComponent<AllCustomersMovingManager>();
        }
        void OnEnable()
        {
            // Запрещаем двигаться. Устанавливаем текущий маршрут. Задаём начальную позицию

            allowMoving = false;
            IdOfCurrentRoute = 0;
            IdOfCurrentGoalPoint = 0;
            moveVector = Vector3.zero;
            transformComponent.position = allCustomerMovingManager.Routes[IdOfCurrentRoute][IdOfCurrentGoalPoint];
            EnterBuilding();
        }
        void FixedUpdate()
        {
            // Рассчитываем расстояние между текущей позицией клиента и целевой точкой
            // Если разрешено двигаться и клиент не слишком близко к целевой точке - двигаемся с заданной скоростью
            // Если же клиент очень близко или уже на целевой точке, то: если она не крайняя в маршруте - начинаем двигаться к следующей
            // Если мы на последней точки маршрута ВХОДА - делаем заказ
            // В иных случаях стоим на месте

            Vector3 deltaVector = transformComponent.position - allCustomerMovingManager.Routes[IdOfCurrentRoute][IdOfCurrentGoalPoint];
            float vectorLength = deltaVector.magnitude;

            if (allowMoving)
            {
                if (vectorLength > 0.25f) rigidbodyComponent.velocity = moveVector * allCustomerMovingManager.speed;
                else
                {
                    if ((IdOfCurrentRoute == 0) && (IdOfCurrentGoalPoint == allCustomerMovingManager.Routes[0].Length - 1))
                    {
                        customerComponent.MakeAnOrder();
                    }
                    if (!CheckIfCurrentGoalPointIsLast())
                    {
                        IdOfCurrentGoalPoint += 1;
                        CalculateMoveVector();
                    }
                    else rigidbodyComponent.velocity = Vector3.zero;
                }
            }
            else rigidbodyComponent.velocity = Vector3.zero;
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

            yield return null;
            allowMoving = true;
            IdOfCurrentGoalPoint = 0;
        }
        private IEnumerator ExitBuildingCoroutine()
        {
            // Запускаем процесс выхода из здания

            IdOfCurrentRoute = 1;
            IdOfCurrentGoalPoint = 0;
            customerSpawner.TakeCustomer();
            yield return new WaitForSeconds(2.5f);
            customerSpawner.PutCustomer();
        }

        #endregion
    }
}
