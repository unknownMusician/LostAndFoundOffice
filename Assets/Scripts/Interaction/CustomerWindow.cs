using Interaction.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction {
    public sealed class CustomerWindow : MonoBehaviour, IReceivable {

        public static CustomerWindow Window { get; private set; } = null;

        private CustomerSpawning.Customer customer = null;
        private bool customerGavePainting = false;

        private void Awake() => Window = this;
        private void OnDestroy() => Window = null;

        #region IInteractable

        public InteractionType[] HowToInteract() {
            var types = new List<InteractionType>();

            if (customer != null) { types.Add(customerGavePainting ? InteractionType.Receive : InteractionType.Grab); }

            return types.ToArray();
        }

        #endregion

        #region IReceivable

        public void Receive(IGrabbable placeable) {
            SendItemToManager(placeable as Item);
        }

        public bool TryReceive() {
            return customer != null && customerGavePainting;
        }

        #endregion

        public void ReceiveOrder(CustomerSpawning.Customer customer) {
            print($"Received order from customer: \"{customer}\"");
            this.customer = customer;
            DataManager.Manager.NextItem();
            customerGavePainting = true;
            waitCoroutine = StartCoroutine(WaitCoroutine(customer));
            // TODO
        }

        private void SendItemToManager(Item item) {
            if (item != null) { item.Grab(customer.transform, Vector3.forward); }
            customer.ReceiveAnswer(DataManager.Manager.ItemGiven(item?.gameObject));
            if (waitCoroutine != null) {
                StopCoroutine(waitCoroutine);
                waitCoroutine = null;
            }
            customer = null;
            // TODO
        }

        private Coroutine waitCoroutine = null; // todo
        private IEnumerator WaitCoroutine(CustomerSpawning.Customer customer) { // todo
            yield return new WaitForSeconds(10);
            DeclineOrder();
        }

        public void DeclineOrder() {
            SendItemToManager(null);
            waitCoroutine = null;
        }
    }
}
