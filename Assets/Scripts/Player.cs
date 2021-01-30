using Interaction.Service;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Interacter))]
public sealed class Player : MonoBehaviour {
    private Rigidbody rigidBody = null;
    private Interacter interacter = null;

    [SerializeField] private float speed;


    private Vector3 moveDampVelocity = default;
    private Vector3 rotateDampVelocity = default;

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
        Vector3 LookVector = Vector3.SmoothDamp(transform.rotation * Vector3.forward, new Vector3(move.x, 0, move.y), ref rotateDampVelocity, 0.1f);
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, LookVector);
        
    }

    private void InteractUpdate() {
        if (Input.GetButtonDown("GrabPlace")) { interacter.Interact(); }
    }
}
