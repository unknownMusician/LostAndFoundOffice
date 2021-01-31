using Interaction.Service;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Interacter))]
[RequireComponent(typeof(Animator))]
public sealed class Player : MonoBehaviour {
    private Rigidbody rigidBody = null;
    private Interacter interacter = null;
    private Animator animator = null;

    [SerializeField] private float speed;


    private Vector3 moveDampVelocity = default;
    private Vector3 rotateDampVelocity = default;

    private void Awake() {
        rigidBody = GetComponent<Rigidbody>();
        interacter = GetComponent<Interacter>();
        animator = transform.Find("Model").GetComponent<Animator>();
    }

    private void Update() {
        MoveUpdate();
        InteractUpdate();
        ExitUpdate();
    }

    private void MoveUpdate() {
        Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed;
        animator.SetFloat("currentSpeed", move.magnitude);
        rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, new Vector3(move.x, rigidBody.velocity.y, move.y), ref moveDampVelocity, 0.1f);
        rigidBody.angularVelocity = Vector3.zero;
        if (move.magnitude == 0) { return; }
        Vector3 LookVector = Vector3.SmoothDamp(transform.rotation * Vector3.forward, new Vector3(move.x, 0, move.y), ref rotateDampVelocity, 0.1f);
        float angle = Vector2.SignedAngle(Service.Project(LookVector), Vector2.up);
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    private void InteractUpdate() {
        if (Input.GetButtonDown("GrabPlace")) { interacter.Interact(); }
    }

    private void ExitUpdate()
    {
        if (Input.GetButtonDown("Exit"))
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}
