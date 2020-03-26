using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private CharacterController controller;

    public Transform groundCheck;
    public LayerMask groundMask;

    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float friction = 20f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float groundDistance = 0.6f;
    private bool isGrounded;

    private Vector3 velocity;

    void FixedUpdate()
    {
        RaycastHit hit;
        float distance = .25f;

        if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
        {
            if (!hit.collider.gameObject.GetComponent<Enemy>() && hit.collider.gameObject.transform.parent != null && !hit.collider.gameObject.transform.parent.GetComponent<Enemy>())
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
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
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        if (velocity.x > 0)
        {
            velocity.x -= friction * Time.deltaTime;
        }
        if (velocity.x < 0)
        {
            velocity.x += friction * Time.deltaTime;
        }

        if (velocity.z > 0)
        {
            velocity.z -= friction * Time.deltaTime;
        }
        if (velocity.z < 0)
        {
            velocity.z += friction * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    public void addVelocity(Vector3 velocity)
    {
        this.velocity += velocity;
    }
}
