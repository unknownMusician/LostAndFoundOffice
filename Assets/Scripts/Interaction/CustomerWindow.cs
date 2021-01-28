using Interaction.Service;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction {
    public sealed class CustomerWindow : MonoBehaviour, IReceivable {

        public static CustomerWindow Window { get; private set; } = null;

        private CustomerSpawning.Customer customer = null;
        private bool customerGavePainting = false;

        private void Awake() => Window = this;
        private void OnDestroy() => Window = null;

        public InteractionType[] HowToInteract() {
            var types = new List<InteractionType>();

            if (customer != null) { types.Add(customerGavePainting ? InteractionType.Receive : InteractionType.Grab); }

            return types.ToArray();
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
            // customer.ReceiveAnswer();
        }
    }
}
