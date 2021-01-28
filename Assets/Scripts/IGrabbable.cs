using UnityEngine;

public interface IGrabbable : IInteractable {

    Item Grab(Transform whoGrabbed, Vector3 newLocalPos);
    bool TryGrab();
}
