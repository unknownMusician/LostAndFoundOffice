using UnityEngine;

public class Pile : Grabbable {

    [SerializeField] private Item item;

    public override GrabbablePlaceable Grab() {
        return Instantiate(item.gameObject).GetComponent<GrabbablePlaceable>();
    }
}
