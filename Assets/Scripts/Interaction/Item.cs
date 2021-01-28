using Interaction.Service;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction {
    [RequireComponent(typeof(Rigidbody))]
    public class Item : MonoBehaviour, IDroppable, IGrabbable {

        protected new Rigidbody rigidbody;
        protected new Collider collider;

        protected ItemState state = ItemState.Dropped;

        protected void Awake() {
            rigidbody = GetComponent<Rigidbody>();
            collider = GetComponent<Collider>();
        }

        #region IInteractable

        public InteractionType[] HowToInteract() {
            var types = new List<InteractionType>();

            // TODO: correct order
            if (TryDrop()) { types.Add(InteractionType.Drop); }
            if (TryGrab()) { types.Add(InteractionType.Grab); }

            return types.ToArray();
        }

        #endregion

        #region IDroppable

        public void Drop() {
            state = ItemState.Dropped;
            transform.SetParent(null);
            rigidbody.isKinematic = false;
            collider.gameObject.layer = LayerMask.NameToLayer("Items");
        }

        public bool TryDrop() {
            return state == ItemState.Holded;
        }

        #endregion

        #region IGrabbable

        public Item Grab(Transform whoInteracted, Vector3 newLocalPos) {
            state = ItemState.Holded;
            transform.SetParent(whoInteracted);
            transform.localPosition = newLocalPos;
            transform.localRotation = Quaternion.identity;
            rigidbody.isKinematic = true;
            collider.gameObject.layer = LayerMask.NameToLayer("TransparentItems");
            return this;
        }

        public bool TryGrab() {
            return state != ItemState.Holded;
        }

        #endregion

        protected enum ItemState {
            Dropped,
            Holded
        }
    }
}