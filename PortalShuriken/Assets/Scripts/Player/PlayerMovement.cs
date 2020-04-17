using UnityEngine;

// This script moves the character controller forward
// and sideways based on the arrow keys.
// It also jumps when pressing space.
// Make sure to attach a character controller to the same game object.
// It is recommended that you make only one call to Move or SimpleMove per frame.

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;


    public Transform groundCheck;
    public LayerMask groundMask;

    public float speed = 10f;
    public float gravity = -20f;
    public float jumpHeight = 2f;
    public float groundDistance = 0.6f;

    private bool isGrounded;
    private Vector3 velocity;
    private float lastFallVelocity = 0;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
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

        if (velocity.y < -20 || lastFallVelocity < -0)
        {
            CalculateFallDamage();
        }
    }

    private void CalculateFallDamage()
    {
        if (velocity.y > lastFallVelocity)
        {
            GetComponent<Player>().UpdateHealth(-(int)Mathf.Pow(lastFallVelocity / 5f, 2));
        }
        lastFallVelocity = velocity.y;
    }
}
