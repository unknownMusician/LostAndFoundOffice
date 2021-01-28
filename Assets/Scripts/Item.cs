using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Item : MonoBehaviour, IDroppable, IGrabbable {

    protected new Rigidbody rigidbody;

    protected ItemState state = ItemState.Dropped;

    protected void Awake() {
        rigidbody = GetComponent<Rigidbody>();
    }

    #region IInteractable

    public InteractionType[] HowToInteract() {
        var types = new List<InteractionType>();

        // todo: correct order
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
