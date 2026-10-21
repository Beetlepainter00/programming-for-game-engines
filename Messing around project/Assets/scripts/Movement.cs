using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;       // Changed to float for smoother 2D movement
    [SerializeField] private float jumpPower = 10f;   // Increased slightly to fight Unity gravity

    public string groundTag = "ground"; // Make sure your floor object is tagged "test" in Unity!
    private int groundContacts = 0;
    public bool isGrounded => groundContacts > 0;

    private Rigidbody2D player;
    private float horizontalInput;
    [SerializeField] 
    private float terminalVelocity = -22f;
    private Animator movementAnimation;
    private bool lookingRight = true;
    void Start()
    {
        // Automatically grabs the Rigidbody2D attached to your player
        player = GetComponent<Rigidbody2D>();
        movementAnimation = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. Get continuous left/right input (-1 for A, 1 for D, 0 for nothing)

        Vector3 moveDir = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {

            moveDir.x = -speed;
            Debug.Log("Left!");
            lookingRight = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDir.x = speed;
            Debug.Log("Right!");
            lookingRight = true;
        }

        if (movementAnimation != null)
        {
            movementAnimation.SetBool("lookingRight", lookingRight);
            movementAnimation.SetBool("grounded", isGrounded);
        }


        
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {

            
            player.linearVelocity = new Vector2(player.linearVelocity.x, jumpPower);
            Debug.Log("Jumping!");
            if (movementAnimation != null)
            {
                movementAnimation.SetTrigger("doJump");
               
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && player.linearVelocity.y > 0)
        {
            // By changing the multiplier math to 0f, the player halts immediately in mid-air
            player.linearVelocity = new Vector2(player.linearVelocity.x, 0f);
        }

        player.linearVelocity = new Vector2(moveDir.x, player.linearVelocity.y);
        if (player.linearVelocity.y < terminalVelocity)
        {
            player.linearVelocity = new Vector2(player.linearVelocity.x, terminalVelocity);
        }
    }



    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            groundContacts++;
            if (movementAnimation != null)
            {
                movementAnimation.ResetTrigger("doJump");
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            groundContacts--;
            if (groundContacts < 0)
            {
                groundContacts = 0;
            }
        }
    }
}