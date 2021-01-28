using UnityEngine;


namespace CustomerSpawning
{
    public class AllCustomersMovingManager : MonoBehaviour
    {
        public Vector3[] enterRoute;                // Маршрут ВХОДА в здание
        public Vector3[] exitRoute;                 // Маршрут ВЫХОДА из здания
        public Vector3[][] Routes { get; set; }

        public float speed;

        void Awake()
        {
            Routes = new Vector3[2][];
            Routes[0] = enterRoute;
            Routes[1] = exitRoute;
        }
    }
}
