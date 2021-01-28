using Interaction.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Interacter))]
public sealed class Player : MonoBehaviour {
    private Rigidbody rigidBody = null;
    private Interacter interacter = null;

    [SerializeField] private float speed;


    private Vector3 moveDampVelocity = default;

    private void Awake() {
        rigidBody = GetComponent<Rigidbody>();
        interacter = GetComponent<Interacter>();
    }

    private void Update() {
        MoveUpdate();
        InteractUpdate();
    }

    private void MoveUpdate() {
        Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed;

        rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, new Vector3(move.x, rigidBody.velocity.y, move.y), ref moveDampVelocity, 0.1f);
        rigidBody.angularVelocity = Vector3.zero;
        if (move.magnitude == 0) { return; }
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z));
    }

    private void InteractUpdate() {
        if (Input.GetButtonDown("GrabPlace")) { interacter.Interact(); }
    }
}
