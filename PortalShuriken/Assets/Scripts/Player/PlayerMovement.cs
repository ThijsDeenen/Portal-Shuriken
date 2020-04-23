using UnityEngine;

// This script moves the character controller forward
// and sideways based on the arrow keys.
// It also jumps when pressing space.
// Make sure to attach a character controller to the same game object.
// It is recommended that you make only one call to Move or SimpleMove per frame.

public class PlayerMovement : MonoBehaviour
{
    public Transform groundCheck;
    public LayerMask groundMask;

    private CharacterController controller;

    public float crouchSpeed = 4f;
    public float walkSpeed = 7f;
    public float runSpeed = 10f;
    public float gravity = -30f;
    public float jumpHeight = 2f;
    public float groundDistance = 0.6f;

    private bool isGrounded;
    private bool isCrouching;
    private float speed;
    private Vector3 velocity;
    private float cHeight;
    private Vector3 cCenter;
    private Vector3 cPosition;
    private float lastFallVelocity = 0;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = walkSpeed;
        cHeight = controller.height;
        cCenter = controller.center;
        cPosition = groundCheck.localPosition;
    }

    private void Update()
    {
        GroundCheck();

        if (isGrounded)
        {
            if (velocity.y < 0)
            {
                velocity.y = 0f;
            }

            if (Input.GetButtonDown("Jump") && !isCrouching)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runSpeed;
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                if (!isCrouching)
                {
                    speed = crouchSpeed;
                    groundCheck.localPosition = new Vector3(groundCheck.localPosition.x, groundCheck.localPosition.y + 1f, groundCheck.localPosition.z);
                    controller.center = new Vector3(controller.center.x, controller.center.y + .5f, controller.center.z);
                    controller.height -= 1f;
                    isCrouching = true;
                }
            }
            else
            {
                speed = walkSpeed;

                if (isCrouching)
                {
                    groundCheck.localPosition = new Vector3(groundCheck.localPosition.x, groundCheck.localPosition.y - 4f * Time.deltaTime, groundCheck.localPosition.z);
                    controller.center = new Vector3(controller.center.x, controller.center.y - 2f * Time.deltaTime, controller.center.z);
                    controller.height += 4f * Time.deltaTime;
                    velocity.y = 3f;
                    if (controller.center.y <= cCenter.y)
                    {
                        controller.height = cHeight;
                        controller.center = cCenter;
                        groundCheck.localPosition = cPosition;
                        isCrouching = false;
                    }
                }
            }
        }
        else
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

    private void GroundCheck()
    {
        Ray ray = new Ray(groundCheck.position, Vector3.down);
        isGrounded = Physics.SphereCast(ray, groundDistance, groundDistance, groundMask);
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
