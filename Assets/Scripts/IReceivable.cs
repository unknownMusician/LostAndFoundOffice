using UnityEngine;

public interface IReceivable : IInteractable {
    void Receive(IGrabbable placeable);
    bool TryReceive();
}
