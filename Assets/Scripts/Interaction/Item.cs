using Interaction.Service;
using System.Collections;
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
            // rigidbody.isKinematic = false;
            var joint = GetComponent<FixedJoint>();
            if (joint != null) {
                joint.connectedBody = null;
                StartCoroutine(DestroyJointIfNoConnectedBody(joint));
            }
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
            // rigidbody.isKinematic = true;
            FixedJoint joint;
            if (GetComponent<FixedJoint>() == null) {
                joint = gameObject.AddComponent<FixedJoint>();
            } else {
                joint = GetComponent<FixedJoint>();
            }
            joint.connectedBody = whoInteracted.GetComponent<Rigidbody>();

            collider.gameObject.layer = LayerMask.NameToLayer("TransparentItems");
            return this;
        }

        public bool TryGrab() {
            return state != ItemState.Holded;
        }

        #endregion

        protected IEnumerator DestroyJointIfNoConnectedBody(FixedJoint joint) {
            yield return null;
            if(joint.connectedBody == null) { Destroy(joint); }
        }

        public void EndItem() {
            if(GetComponent<FixedJoint>() != null) { Destroy(GetComponent<FixedJoint>()); }
            if(GetComponent<Rigidbody>() != null) { Destroy(GetComponent<Rigidbody>()); }
            transform.SetParent(null);
        }

        protected enum ItemState {
            Dropped,
            Holded
        }
    }
}