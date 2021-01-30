using UnityEngine;


namespace CustomerSpawning
{
    public class Customer : MonoBehaviour
    {
        private Rigidbody rigidbodyComponent;
        private CustomerMovement movementComponent;

        public bool AllowRotation { get; set; }

        void Awake()
        {
            rigidbodyComponent = GetComponent<Rigidbody>();
            movementComponent = GetComponent<CustomerMovement>();

            rigidbodyComponent.freezeRotation = true;
        }
        void OnEnable()
        {
            AllowRotation = true;
        }
        void OnDisable()
        {
            if (transform.childCount == 3) Destroy(transform.GetChild(2).gameObject);
        }
        void FixedUpdate()
        {
            if ((rigidbodyComponent.velocity != Vector3.zero) && (AllowRotation))
            {
                float angle = Vector2.SignedAngle(Service.Project(rigidbodyComponent.velocity), Vector2.up);
                transform.rotation = Quaternion.Euler(0, angle, 0);            
            }
        }

        public void MakeAnOrder()
        {
            Interaction.CustomerWindow.Window.ReceiveOrder(this);
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
