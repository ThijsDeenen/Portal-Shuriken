using UnityEngine;
using System.Collections;

// This script moves the character controller forward
// and sideways based on the arrow keys.
// It also jumps when pressing space.
// Make sure to attach a character controller to the same game object.
// It is recommended that you make only one call to Move or SimpleMove per frame.

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInputHor;
    private float moveInputVer;
    private Rigidbody rb;
    private float horizontalMove;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        moveInputHor = Input.GetAxis("Horizontal");
        moveInputVer = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(moveInputHor * speed, rb.velocity.y, moveInputVer * speed);
        //horizontalMove = moveInput * speed;
    }
}
