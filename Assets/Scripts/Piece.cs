using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : GrabbablePlaceable {

    private PieceState state = PieceState.InHands;

    public override bool Place(Vector2 position) {
        if (state == PieceState.OnFloor) { return false; }
        transform.position = new Vector3(position.x, 0.1f, position.y);
        transform.rotation = Quaternion.identity;
        state = PieceState.OnFloor;
        return true;
    }

    public override GrabbablePlaceable Grab() {
        if (state == PieceState.InHands) { return null; }

        state = PieceState.InHands;
        return this;
    }

    private enum PieceState {
        OnFloor,
        InHands
    }
}
