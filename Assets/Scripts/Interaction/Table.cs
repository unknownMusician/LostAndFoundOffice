using Interaction.Service;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction {
    public class Table : MonoBehaviour, IGrabbable, IReceivable {

        [SerializeField] private Vector3 itemLocalPos = Vector3.up;

        private Item holdedItem = null;

        #region IInteractable

        public InteractionType[] HowToInteract() {
            var types = new List<InteractionType>();

            // TODO: correct order
            if (TryReceive()) { types.Add(InteractionType.Receive); }
            if (TryGrab()) { types.Add(InteractionType.Grab); }

            return types.ToArray();
        }

        #endregion

        #region IGrabbable

        public Item Grab(Transform whoGrabbed, Vector3 newLocalPos) {
            var item = holdedItem.Grab(whoGrabbed, newLocalPos);
            holdedItem = null;
            return item;
        }

        public bool TryGrab() {
            return holdedItem != null;
        }

        #endregion

        #region IReceivable

        public void Receive(IGrabbable grabbable) {
            grabbable.Grab(transform, itemLocalPos);
            holdedItem = grabbable as Item;
        }

        public bool TryReceive() {
            return holdedItem == null;
        }

        #endregion

        protected void OnDrawGizmos() {
            Gizmos.color = holdedItem == null ? Color.cyan : Color.yellow;
            Gizmos.DrawWireSphere(transform.position + itemLocalPos, 0.5f);
        }
    }
}