using UnityEngine;

public class PlayerAlec : MonoBehaviour
{
    private Rigidbody2D player;
    [SerializeField]
    private float speed = 2.8f;
    [SerializeField]
    private float jumpForce = 6.65f;
    private float jumpTime = 0f;
    private float minJumpTime = 0.05f;

    private bool jumping = false;


    private float groundCushionTime = 0.05f;
    private float lastGroundedTime;

    private Vector3 spawnLocation;
    private Vector3 currentSpawnLocation;
    void Start()
    {
        spawnLocation.x = 7;
        currentSpawnLocation.x = 7;
        currentSpawnLocation.y = -3.5f;
        spawnLocation.y = -3.5f;
        player = GetComponent<Rigidbody2D>();
        GameManager.Instance.player2 = this.transform;
    }
    public void TeleportToNextLevel(float xOffsetAmount)
    {
        // Add to our base spawn location
        currentSpawnLocation.x += xOffsetAmount;

        // Instantly move the player to the new position
        this.transform.position = currentSpawnLocation;

        // Reset physical speed so they don't slide upon arrival
        if (player != null)
        {
            player.linearVelocity = Vector2.zero;
        }
    }
    public void BackToStart()
    {
        // Add to our base spawn location
        currentSpawnLocation = spawnLocation;

        // Instantly move the player to the new position
        this.transform.position = currentSpawnLocation;
    }
    public Collider2D playerCol;

    void Update()
    {

        bool isGrounded = GetOnFloor();

        // If the contact check is true, refresh our safety clock
        if (isGrounded)
        {
            lastGroundedTime = Time.time;
        }
        if (GameManager.Instance != null && !GameManager.Instance.canMove)
        {

            player.linearVelocityX = 0;
            return;
        }

        bool canJump = (Time.time - lastGroundedTime <= groundCushionTime);


        if (canJump && player.linearVelocityY <= 0.05f)
        {
            jumping = false;
        }

        // Jump 
        if (!Input.GetKey(KeyCode.I) && Time.time - jumpTime >= minJumpTime && player.linearVelocityY > 0.05f)
        {
            player.linearVelocityY = 0;
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            if (canJump && !jumping)
            {
                jumping = true;
                jumpTime = Time.time;
                player.linearVelocityY = jumpForce;
                player.linearVelocityX *= 1.05f;
            }
        }

        // Horizontal Movement
        if (Input.GetKey(KeyCode.J))
        {
            player.linearVelocityX = -speed;
        }
        else if (Input.GetKey(KeyCode.L))
        {
            player.linearVelocityX = speed;
        }
        else
        {
            player.linearVelocityX = 0;
        }
    }


    public bool GetOnFloor()
    {
        ContactPoint2D[] contacts = new ContactPoint2D[10];
        int count = playerCol.GetContacts(contacts);

        bool onFloor = false;

        for (int i = 0; i < count; i++)
        {
            Vector2 normal = contacts[i].normal;

            if (normal.y < 0.4f)
                continue;

            onFloor = true;
            break;
        }

        return onFloor;
    }
}
