using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerAviv : MonoBehaviour
{
    [Header("Gameplay Variables")]
    [SerializeField] private int jumpForce = 5;
    [SerializeField] private float gravityMultiplier = 1;
    [SerializeField] private int moveSpeed = 5;

    private Rigidbody rigidBody;
    private bool isGrounded = false;
    // private bool isDefending = false;
    // private Quaternion rotation;

    void Start() {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void OnJump(InputValue jumpValue) {
        if (isGrounded) {
            rigidBody.AddForce(Vector3.up * jumpForce / gravityMultiplier, ForceMode.VelocityChange);
            isGrounded = false;

        }
    }

    public void OnMove(InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>();

        rigidBody.velocity = new Vector3(movementVector.x * moveSpeed, rigidBody.velocity.y, 0);

    }

    public void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Platform")) {
            isGrounded = true;

            //maintain  horizontal velocity when hitting a platform to keep movement smooth
            rigidBody.velocity = new Vector3(-other.relativeVelocity.x, 0, 0);
        }

    }
}
