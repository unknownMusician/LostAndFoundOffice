using System.Collections;
using UnityEngine;

namespace CustomerSpawning
{
    public class CustomerMovement : MonoBehaviour
    {
        #region Properties

        private Rigidbody rigidbodyComponent;
        private Transform transformComponent;

        private CustomerSpawningManager customerSpawner;
        private AllCustomersMovingManager customerMovingManager;

        private bool allowMoving;
        private int IdOfCurrentRoute;                       // ID текущего маршрута (0 - вход, 1 - выход)
        private int IdOfCurrentGoalPoint;                   // ID текущей целевой точки в маршруте
        private Vector3 moveVector;                         // Вектор, направляющий customer'a от одной точки к другой

        private Coroutine testCycleCoroutine;               // ТЕСТОВАЯ ПЕРЕМЕННАЯ! Нужна для проверки алгоритма. Хранит в себе корутину цикла спавна клиентов

        #endregion

        #region Behaviour methods

        void Awake()
        {
            rigidbodyComponent = GetComponent<Rigidbody>();
            transformComponent = GetComponent<Transform>();

            customerSpawner = transformComponent.parent.parent.GetComponent<CustomerSpawningManager>();
            customerMovingManager = transformComponent.parent.parent.GetComponent<AllCustomersMovingManager>();
        }
        void OnEnable()
        {
            // Запрещаем двигаться. Устанавливаем текущий маршрут. Задаём начальную позицию

            allowMoving = false;
            IdOfCurrentRoute = 0;
            IdOfCurrentGoalPoint = 0;
            moveVector = Vector3.zero;
            transformComponent.position = customerMovingManager.Routes[IdOfCurrentRoute][IdOfCurrentGoalPoint];
            testCycleCoroutine = StartCoroutine(EnterAndExitTestCycle());
        }
        void OnDisable()
        {
            StopCoroutine(testCycleCoroutine);
        }
        void FixedUpdate()
        {
            // Рассчитываем расстояние между текущей позицией клиента и целевой точкой
            // Если разрешено двигаться и клиент не слишком близко к целевой точке - двигаемся с заданной скоростью
            // Если же клиент очень близко или уже на целевой точке, то: если она не крайняя в маршруте - начинаем двигаться к следующей
            // В иных случаях стоим на месте

            Vector3 deltaVector = transformComponent.position - customerMovingManager.Routes[IdOfCurrentRoute][IdOfCurrentGoalPoint];
            float vectorLength = deltaVector.magnitude;

            if(allowMoving)
            {
                if(vectorLength > 0.25f) rigidbodyComponent.velocity = moveVector * customerMovingManager.speed;
                else
                {
                    if(!CheckIfCurrentGoalPointIsLast())
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

            return IdOfCurrentGoalPoint + 1 >= customerMovingManager.Routes[IdOfCurrentRoute].Length;
        }
        private void CalculateMoveVector()
        {
            // Рассчитываем направляющий вектор

            if(IdOfCurrentGoalPoint != 0)
            {
                moveVector = customerMovingManager.Routes[IdOfCurrentRoute][IdOfCurrentGoalPoint] - customerMovingManager.Routes[IdOfCurrentRoute][IdOfCurrentGoalPoint - 1];
                NormalizeMoveVector();
            }
        }
        private void NormalizeMoveVector()
        {
            // Нормализуем направляющий вектор, чтобы его координаты не были больше 1

            if(Mathf.Abs(moveVector.x) >= Mathf.Abs(moveVector.z))
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
            yield return new WaitForSeconds(10);
            customerSpawner.PutCustomer();
        }

        private IEnumerator EnterAndExitTestCycle()
        {
            // ТЕСТОВЫЙ МЕТОД!!! Отвечающий за цикл входа и выхода
            EnterBuilding();
            yield return new WaitForSeconds(11);
            ExitBuilding();
        }

        #endregion
    }
}
