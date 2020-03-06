using UnityEngine;
using System.Collections;

// This script moves the character controller forward
// and sideways based on the arrow keys.
// It also jumps when pressing space.
// Make sure to attach a character controller to the same game object.
// It is recommended that you make only one call to Move or SimpleMove per frame.

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float speed = 1;
    [SerializeField] private float jumpForce;
    private Player player;
    private float moveInputHor;
    private float moveInputVer;
    private float horizontalMove;

    private void Start()
    {
        player = GetComponent<Player>();
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        moveInputHor = Input.GetAxis("Horizontal");
        moveInputVer = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(moveInputHor * speed, rb.velocity.y, moveInputVer * speed);
        //horizontalMove = moveInput * speed;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<Enemy>())
        {
            player.UpdateHealth(-20);
        }
    }
}
