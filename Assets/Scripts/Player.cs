using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float speed;

    private Rigidbody rigidBody = null;

    private Vector3 moveDampVelocity = default;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MoveUpdate();
    }

    private void MoveUpdate()
    {
        Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed;

        rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, new Vector3(move.x, rigidBody.velocity.y, move.y), ref moveDampVelocity, 0.1f);
        rigidBody.angularVelocity = Vector3.zero;
        if (move.magnitude == 0) { return; }
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z));
    }

    private Vector2 Round(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x + 0.5f) - 0.5f, Mathf.Round(v.y + 0.5f) - 0.5f);
    }
}
