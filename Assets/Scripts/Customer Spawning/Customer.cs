using UnityEngine;


namespace CustomerSpawning
{
    public class Customer : MonoBehaviour
    {
        private Rigidbody rigidbodyComponent;
        private CustomerMovement movementComponent;

        private bool isOrderMade;

        void Awake()
        {
            rigidbodyComponent = GetComponent<Rigidbody>();
            movementComponent = GetComponent<CustomerMovement>();

            rigidbodyComponent.freezeRotation = true;
        }
        void OnEnable()
        {
            isOrderMade = false;
        }
        void OnDisable()
        {
            if (transform.childCount == 3) Destroy(transform.GetChild(2).gameObject);
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
                Interaction.CustomerWindow.Window.ReceiveOrder(this);
                Debug.Log("I've made an order!");
                isOrderMade = true;
            }
        }
        public void ReceiveAnswer(bool? answer)
        {
            switch (answer)
            {
                case true:
                    Debug.Log("I've received right object!!! :)");
                    break;
                case false:
                    Debug.Log("I've received wrong object!!! :(");
                    break;
                case null:
                    Debug.Log("I haven't received anything!!! :(");
                    break;
            }
            movementComponent.ExitBuilding();
        }
    }
}
