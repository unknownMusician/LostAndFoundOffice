using System.Collections;
using UnityEngine;


public class CustomerMovement : MonoBehaviour
{
    #region Properties

    private Rigidbody rigidbodyComponent;
    private Transform transformComponent;

    private CustomerSpawningManager customerSpawner;
    private AllCustomersMovingManager customerMovingManager;

    private bool allowMoving;
    private int IDOfCurrentRoute;                       // ID текущего маршрута (0 - вход, 1 - выход)
    private int IDOfCurrentGoalPoint;                   // ID текущей целевой точки в маршруте
    private Vector3 moveVector;                         // Вектор, направляющий customer'a от одной точки к другой

    private Coroutine testCycleCoroutine;

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
        allowMoving = false;
        IDOfCurrentRoute = 0;
        IDOfCurrentGoalPoint = 0;
        moveVector = Vector3.zero;
        transformComponent.position = customerMovingManager.enteringTheBuildingRoute[IDOfCurrentGoalPoint];
        testCycleCoroutine = StartCoroutine(EnterAndExitTestCycle());
    }
    void OnDisable()
    {
        StopCoroutine(testCycleCoroutine);
    }
    void FixedUpdate()
    {
        Vector3 deltaVectorBetweenCurrentPositionAndGoalPoint = transformComponent.position - customerMovingManager.Routes[IDOfCurrentRoute][IDOfCurrentGoalPoint];
        float vectorLength = deltaVectorBetweenCurrentPositionAndGoalPoint.magnitude;
        if (vectorLength > 0.25f)
        {
            rigidbodyComponent.velocity = moveVector * customerMovingManager.speed;
        }
        else
        {
            if ((allowMoving) && (!CheckIfCurrentGoalPointIsLast()))
            {
                IDOfCurrentGoalPoint += 1;
                CalculateMoveVector();
            }
            else
            {
                rigidbodyComponent.velocity = Vector3.zero;
            }
        }
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
        return IDOfCurrentGoalPoint + 1 >= customerMovingManager.Routes[IDOfCurrentRoute].Length;
    }
    private void CalculateMoveVector()
    {
        if (IDOfCurrentGoalPoint != 0)
        {
            moveVector = customerMovingManager.Routes[IDOfCurrentRoute][IDOfCurrentGoalPoint] - customerMovingManager.Routes[IDOfCurrentRoute][IDOfCurrentGoalPoint - 1];
        }
    }

    private IEnumerator EnterBuildingCoroutine()
    {
        yield return null;
        allowMoving = true;
        IDOfCurrentGoalPoint = 0;
        CalculateMoveVector();
    }
    private IEnumerator ExitBuildingCoroutine()
    {
        IDOfCurrentRoute = 1;
        IDOfCurrentGoalPoint = 0;
        customerSpawner.TakeNewCustomerFromPoolAndConfigureHim();
        yield return new WaitForSeconds(5);
        customerSpawner.PutOldCustomerToPool();
    }

    private IEnumerator EnterAndExitTestCycle()
    {
        EnterBuilding();
        yield return new WaitForSeconds(6);
        ExitBuilding();
    }

    #endregion
}
