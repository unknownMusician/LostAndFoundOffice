using UnityEngine;


namespace CustomerSpawning
{
    public class Customer : MonoBehaviour
    {
        private bool isOrderMade;

        void OnEnable()
        {
            isOrderMade = false;
        }

        public void MakeAnOrder()
        {
            if (!isOrderMade)
            {
                Debug.Log("I've made an order!");
                isOrderMade = true;
            }
        }
        public void ReceiveAnswer()
        {
            Debug.Log("received");
        }
    }
}
