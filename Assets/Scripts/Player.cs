using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float speed;

    private Rigidbody rigidBody = null;
    private GrabbableSearcher grabbableSearcher = null;

    private GrabbablePlaceable item = null;
    private Vector3 moveDampVelocity = default;

    private void Awake() {
        rigidBody = GetComponent<Rigidbody>();
        grabbableSearcher = GetComponent<GrabbableSearcher>();
    }

    private void Update() {
        MoveUpdate();
        GrabPlaceUpdate();
    }

    private void MoveUpdate() {
        Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed;

        rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, new Vector3(move.x, rigidBody.velocity.y, move.y), ref moveDampVelocity, 0.1f);
        rigidBody.angularVelocity = Vector3.zero;
        if(move.magnitude == 0) { return; }
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z));
    }

    private void GrabPlaceUpdate() {
        if (Input.GetButtonDown("GrabPlace")) {
            if (item == null) {
                item = grabbableSearcher.Item?.Grab();
                if (item == null) { return; }
                item.transform.SetParent(transform);
                item.transform.localPosition = Vector3.forward;
                item.transform.localRotation = Quaternion.identity;
            } else {
                if (item.Place(Round(new Vector2(transform.position.x, transform.position.z)))) {
                    item.transform.SetParent(null);
                    item = null;
                }
            }
        }
    }

    private Vector2 Round(Vector2 v) {
        return new Vector2(Mathf.Round(v.x + 0.5f) - 0.5f, Mathf.Round(v.y + 0.5f) - 0.5f);
    }
}
