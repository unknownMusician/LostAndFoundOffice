using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : GrabbablePlaceable {

    private PieceState state = PieceState.InHands;

    public override bool Drop()
    {
        if (state == PieceState.OnFloor) { return false; }
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
