using Interaction.Service;
using UnityEngine;

namespace Interaction {
    public sealed class CustomerWindow : MonoBehaviour, IReceivable {

        public static CustomerWindow Window { get; private set; } = null;

        private CustomerSpawning.Customer customer = null;

        private void Awake() => Window = this;
        private void OnDestroy() => Window = null;

        public InteractionType[] HowToInteract() {
            throw new System.NotImplementedException();
        }

        public void Receive(IGrabbable placeable) {
            throw new System.NotImplementedException();
        }

        public bool TryReceive() {
            throw new System.NotImplementedException();
        }

        public void ReceiveOrder(CustomerSpawning.Customer customer) {
            print($"Received order from customer: \"{customer}\"");
            // todo
        }

        private void Test() { // todo
            // if Success (Received right and in time)
            // todo
        }
    }
}
