using UnityEngine;

namespace Interaction {
    public interface IGrabbable : IInteractable {

        Item Grab(Transform whoGrabbed, Vector3 newLocalPos);
        bool TryGrab();
    }
}