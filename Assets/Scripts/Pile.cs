using UnityEngine;

public class Pile : Grabbable {

    [SerializeField] private Piece piece;

    public override GrabbablePlaceable Grab() {
        return Instantiate(piece.gameObject).GetComponent<GrabbablePlaceable>();
    }
}
