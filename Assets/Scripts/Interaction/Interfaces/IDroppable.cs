namespace Interaction {
    public interface IDroppable : IInteractable {

        void Drop();
        bool TryDrop();
    }
}