using UnityEngine;


public class AllCustomersMovingManager : MonoBehaviour
{
    public Vector3[] enteringTheBuildingRoute;
    public Vector3[] exitingTheBuildingRoute;
    public Vector3[][] Routes { get; set; }

    public float speed;

    void Awake()
    {
        Routes = new Vector3[2][];
        Routes[0] = enteringTheBuildingRoute;
        Routes[1] = exitingTheBuildingRoute;
    }
}
