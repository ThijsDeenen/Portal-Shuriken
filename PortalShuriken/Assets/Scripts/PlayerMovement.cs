using System;
using UnityEngine;
using System.Collections;
using Object = System.Object;

// This script moves the character controller forward
// and sideways based on the arrow keys.
// It also jumps when pressing space.
// Make sure to attach a character controller to the same game object.
// It is recommended that you make only one call to Move or SimpleMove per frame.

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Player player;

    public Transform groundCheck;
    public LayerMask groundMask;

    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float groundDistance = 0.6f;
    private bool isGrounded;

    private Vector3 velocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        player = GetComponent<Player>(); 
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit);

        if (hit.gameObject.GetComponent<Enemy>())
        {
            player.UpdateHealth(-20);
        }
    }
}
