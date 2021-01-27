using UnityEngine;

public class Interacter : MonoBehaviour
{
    private GrabbableSearcher grabbableSearcher = null;
    private InteractableSearcher interactableSearcher = null;

    private GrabbablePlaceable item = null;
    private void Awake()
    {
        grabbableSearcher = GetComponent<GrabbableSearcher>();
        interactableSearcher = GetComponent<InteractableSearcher>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("GrabPlace"))
            Interact();
    }

    private void Interact()
    {
        var interact = interactableSearcher.Item;

        if (item == null)
        {
            if (interact != null)
            {
                item = interact.Grab();
            }
            if (item == null)
            {
                item = grabbableSearcher.Item?.Grab();
            }
            if (item == null)
            {
                return;
            }
            var rb = item.GetComponent<Rigidbody>();
            Destroy(rb);
            item.transform.SetParent(transform);
            item.transform.localPosition = Vector3.forward;
            item.transform.localRotation = Quaternion.identity;

        }
        else
        {
            if (interact == null)
            {
                item.Drop();
                item.transform.SetParent(null);
                var rb = item.gameObject.AddComponent<Rigidbody>();
                rb.useGravity = true;
                rb.AddForce(9.8f * Vector3.down, ForceMode.Impulse);
                item = null;
            }
            else
            {
                if (interact.Place(item))
                {
                    item = null;
                }
            }
        }
    }
}
