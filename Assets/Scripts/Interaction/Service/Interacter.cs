using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Interaction.Service {
    [RequireComponent(typeof(InteractableSearcher))]
    public class Interacter : MonoBehaviour {
        protected InteractableSearcher interactableSearcher = null;

        [SerializeField] protected Vector3 itemLocalPos = Vector3.forward;

        protected Item holdedItem = null;

        protected void Awake() {
            interactableSearcher = GetComponent<InteractableSearcher>();
        }

        public void Interact() {
            var howCanIInteract = HowCanIInteract();
            var itemToInteract = interactableSearcher.GetItem(howCanIInteract, holdedItem);

            if (itemToInteract == null) {
                if (holdedItem != null) {
                    itemToInteract = holdedItem;
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
            if (holdedItem != null) { types.Add(InteractionType.Drop); } // TODO: make smaller
            if (holdedItem == null) { types.Add(InteractionType.Grab); }
            if (holdedItem != null) { types.Add(InteractionType.Receive); }
            types.Add(InteractionType.Decline);

            return types.ToArray();
        }

        protected void Interact(IInteractable itemToInteract, InteractionType type) {
            switch (type) {
                case InteractionType.Grab:
                    holdedItem = (itemToInteract as IGrabbable).Grab(transform, itemLocalPos);
                    break;
                case InteractionType.Drop:
                    (itemToInteract as IDroppable).Drop();
                    holdedItem = null;
                    break;
                case InteractionType.Receive:
                    holdedItem.Drop();
                    (itemToInteract as IReceivable).Receive(holdedItem);
                    holdedItem = null;
                    break;
                case InteractionType.Decline:
                    (itemToInteract as IDeclinable).DeclineOrder();
                    break;
                    
            }
        }

        protected void OnDrawGizmos() {
            Gizmos.color = holdedItem == null ? Color.cyan : Color.yellow;
            Gizmos.DrawWireSphere(transform.position + transform.rotation * itemLocalPos, 0.5f);
        }
    }
}