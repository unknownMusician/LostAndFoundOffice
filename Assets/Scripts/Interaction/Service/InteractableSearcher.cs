using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Interaction.Service {
    public class InteractableSearcher : MonoBehaviour {

        private List<IInteractable> items = new List<IInteractable>();

        public IInteractable GetItem(InteractionType[] possibleTypes, IInteractable exceptThis = default) {
            if (possibleTypes.Length == 0) { return null; }
            GameObject closestItem = null;
            foreach (var item in items) {
                if ((item as MonoBehaviour) == (exceptThis as MonoBehaviour)) { continue; }
                if (item.HowToInteract().Intersect(possibleTypes).Count() == 0) { continue; }
                if (closestItem == null ||
                    (closestItem.transform.position - transform.position).magnitude > ((item as MonoBehaviour).transform.position - transform.position).magnitude
                    ) {
                    closestItem = (item as MonoBehaviour).gameObject;
                }
            }
            return closestItem?.GetComponent<IInteractable>();
        }

        private void OnTriggerEnter(Collider other) {
            var possibleItem = other.GetComponent<IInteractable>();
            if (possibleItem != null) { items.Add(possibleItem); }
        }

        private void OnTriggerExit(Collider other) {
            var possibleItem = other.GetComponent<IInteractable>();
            if (possibleItem != null) { items.Remove(possibleItem); }
        }
    }
}