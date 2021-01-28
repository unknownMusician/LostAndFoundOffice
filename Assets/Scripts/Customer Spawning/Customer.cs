using UnityEngine;


namespace CustomerSpawning
{
    public class Customer : MonoBehaviour
    {
        private Rigidbody rigidbodyComponent;

        private bool isOrderMade;

        void Awake()
        {
            rigidbodyComponent = GetComponent<Rigidbody>();
            rigidbodyComponent.freezeRotation = true;
        }
        void OnEnable()
        {
            isOrderMade = false;
        }
        void FixedUpdate()
        {
            if (rigidbodyComponent.velocity != Vector3.zero)
            {
                transform.rotation = Quaternion.FromToRotation(Vector3.forward, rigidbodyComponent.velocity);
            }
        }

        public void MakeAnOrder()
        {
            if (!isOrderMade)
            {
                // Interaction.CustomerWindow.Window.ReceiveOrder(this);
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
