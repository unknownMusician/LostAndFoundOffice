using System.Collections.Generic;
using UnityEngine;

public class GrabbableSearcher : MonoBehaviour {

    private List<Grabbable> items = new List<Grabbable>();
    public Grabbable Item {
        get {
            Grabbable closestItem = null;
            foreach (var item in items) {
                if (closestItem == null ||
                    (closestItem.transform.position - transform.position).magnitude >
                    (item.transform.position - transform.position).magnitude
                    ) {
                    closestItem = item;
                }
            }
            return closestItem;
        }
    }

    private void OnTriggerEnter(Collider other) {
        var possibleItem = other.GetComponent<Grabbable>();
        if (possibleItem == null) { return; }

        items.Add(possibleItem);
    }

    private void OnTriggerExit(Collider other) {
        var possibleItem = other.GetComponent<Grabbable>();
        if (possibleItem == null) { return; }

        items.Remove(possibleItem);
    }
}
