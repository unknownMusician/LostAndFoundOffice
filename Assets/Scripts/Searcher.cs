using System.Collections.Generic;
using UnityEngine;

public class Searcher<T> : MonoBehaviour where T : MonoBehaviour
{
    private List<T> items = new List<T>();
    public T Item
    {
        get
        {
            T closestItem = null;
            foreach (var item in items)
            {
                if (closestItem == null ||
                    (closestItem.transform.position - transform.position).magnitude >
                    (item.transform.position - transform.position).magnitude
                    )
                {
                    closestItem = item;
                }
            }
            return closestItem;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var possibleItem = other.GetComponent<T>();
        if (possibleItem == null) { return; }

        items.Add(possibleItem);
    }

    private void OnTriggerExit(Collider other)
    {
        var possibleItem = other.GetComponent<T>();
        if (possibleItem == null) { return; }

        items.Remove(possibleItem);
    }
}
