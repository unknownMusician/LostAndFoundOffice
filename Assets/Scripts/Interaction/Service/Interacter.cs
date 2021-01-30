using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Interaction.Service {
    [RequireComponent(typeof(InteractableSearcher))]
    public class Interacter : MonoBehaviour {
        protected InteractableSearcher interactableSearcher = null;

        [SerializeField] protected Vector3 itemLocalPos = Vector3.forward;

        protected Item _holdedItem = null;
        protected Item HoldedItem {
            get => _holdedItem;
            set {
                _holdedItem = value;
                var hands = transform.Find("Model").Find("Hands");
                hands.localPosition = _holdedItem == null ? Vector3.zero : Vector3.forward;
                hands.localScale = _holdedItem == null ? Vector3.one * 100 : Vector3.one * 80;
            }
        }

        protected void Awake() {
            interactableSearcher = GetComponent<InteractableSearcher>();
        }

        public void Interact() {
            var howCanIInteract = HowCanIInteract();
            var itemToInteract = interactableSearcher.GetItem(howCanIInteract, HoldedItem);

            if (itemToInteract == null) {
                if (HoldedItem != null) {
                    itemToInteract = HoldedItem;
                } else { return; }
            }

            var commonTypes = howCanIInteract.Intersect(itemToInteract.HowToInteract());
            if (commonTypes.Count() == 0) { return; }

            Interact(itemToInteract, commonTypes.First());
        }

        protected InteractionType[] HowCanIInteract() {
            var types = new List<InteractionType>();

            // TODO: correct order
            // TODO: actually, can't place (can only grab)
            if (HoldedItem != null) { types.Add(InteractionType.Drop); } // TODO: make smaller
            if (HoldedItem == null) { types.Add(InteractionType.Grab); }
            if (HoldedItem != null) { types.Add(InteractionType.Receive); }
            if (HoldedItem == null) { types.Add(InteractionType.Message); }

            return types.ToArray();
        }

        protected void Interact(IInteractable itemToInteract, InteractionType type) {
            switch (type) {
                case InteractionType.Grab:
                    HoldedItem = (itemToInteract as IGrabbable).Grab(transform, itemLocalPos);
                    break;
                case InteractionType.Drop:
                    (itemToInteract as IDroppable).Drop();
                    HoldedItem = null;
                    break;
                case InteractionType.Receive:
                    HoldedItem.Drop();
                    (itemToInteract as IReceivable).Receive(HoldedItem);
                    HoldedItem = null;
                    break;
                case InteractionType.Message:
                    (itemToInteract as IMessageable).Message();
                    break;

            }
        }

        protected void OnDrawGizmos() {
            Gizmos.color = HoldedItem == null ? Color.cyan : Color.yellow;
            Gizmos.DrawWireSphere(transform.position + transform.rotation * itemLocalPos, 0.5f);
        }
    }
}