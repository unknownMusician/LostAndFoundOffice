namespace Interaction {
    public interface IReceivable : IInteractable {
        void Receive(IGrabbable placeable);
        bool TryReceive();
    }
}