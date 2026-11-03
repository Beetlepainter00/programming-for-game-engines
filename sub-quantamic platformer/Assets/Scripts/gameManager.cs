
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool playersTouching = false;
    [SerializeField]
    private int currentLvl = 1;
  
    public bool canMove { get; private set; } = true;
    private float freezeTimer = 0f;
    [SerializeField] 
    private float freezeDuration = 5f;

    public Transform player1;
    public Transform player2;

    [Header("Cameras Array")]
    // Element 0 = Level 1, Element 1 = Level 2, Element 2 = Level 3, etc.
    public Camera[] levelCameras;
    void Awake()
    {
        Instance = this;
        for (int lvl = 0; lvl < levelCameras.Length ; lvl++)
        {
            if (levelCameras[lvl] != null)
            {
                // Crucial Fix: Turn the underlying component ON for all cameras 
                // so that controlling the GameObject state actually works later!
                levelCameras[lvl].enabled = true;

                if (lvl == currentLvl - 1)
                {
                    Debug.Log("enabling camera" + (lvl));
                    levelCameras[lvl].gameObject.SetActive(true);
                }
                else
                {
                    Debug.Log(" disabling camera" + (lvl));
                    levelCameras[lvl].gameObject.SetActive(false);
                }
            }
        }
    }
    private float levelXDistance = 25f;
    void Update()
    {
        if (!canMove)
        {
            freezeTimer -= Time.deltaTime;
            if (freezeTimer <= 0f)
            {
                canMove = true;
                Debug.Log("can move");
            }
        }
        if (canMove && Input.GetKeyDown(KeyCode.Space))
        {
            SwapPlayerPositions();
        }
        ArePlayersTouching();

    }
    private void ArePlayersTouching()
    {
        if (player1 != null && player2 != null)
        {
            // Calculate the exact distance between the centers of both squares
            float distance = Vector3.Distance(player1.position, player2.position);

            // Assuming your squares are 1x1 size, they overlap if distance is under 1 unit
            if (distance < 1.0f)
            {
                if (!playersTouching)
                {
                    playersTouching = true;
                    Debug.Log("players touched");
                    StartCoroutine(LevelTransition());
                  
                }
            }
            else
            {
                playersTouching = false; // Reset when they separate
            }
        }
    }
    public void SwapPlayerPositions()
    {
       
        if (player1 != null && player2 != null)
        {
            // 1. Save Player 1's position in a temporary variable
            Vector3 tempPosition = player1.position;

            // 2. Move Player 1 to Player 2's position
            player1.position = player2.position;

            // 3. Move Player 2 to the saved temporary position
            player2.position = tempPosition;

            Debug.Log("Players swapped positions!");
        }
    }
    private IEnumerator LevelTransition()
    {
        Debug.Log("tranistioning");
        canMove = false;
        freezeTimer = freezeDuration;

        Rigidbody2D rb1 = player1.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = player2.GetComponent<Rigidbody2D>();

        if (rb1 != null) { rb1.linearVelocity = Vector2.zero; }
        if (rb2 != null) { rb2.linearVelocity = Vector2.zero; }

        yield return new WaitForSeconds(2.0f);

  
        int oldCameraIndex = currentLvl - 1;
        if (oldCameraIndex >= 0 && oldCameraIndex < levelCameras.Length && levelCameras[oldCameraIndex] != null)
        {
            levelCameras[oldCameraIndex].gameObject.SetActive(false);
            Debug.Log("disabaling cams");
        }
        currentLvl = (currentLvl % levelCameras.Length) + 1;

        int newCameraIndex = currentLvl - 1;

        if (newCameraIndex >= 0 && newCameraIndex < levelCameras.Length && levelCameras[newCameraIndex] != null)
        {
            levelCameras[newCameraIndex].gameObject.SetActive(true);
            Debug.Log("enabling cams");
        }


        if (currentLvl != 1)
        {
            if (player1 != null)
            {
                PlayerPro proScript = player1.GetComponent<PlayerPro>();
                if (proScript != null) proScript.TeleportToNextLevel(levelXDistance);
            }

            if (player2 != null)
            {

                PlayerAlec player2Script = player2.GetComponent<PlayerAlec>();
                if (player2Script != null) player2Script.TeleportToNextLevel(levelXDistance);
            }
        }
        else
        {
            if (player1 != null)
            {
                PlayerPro proScript = player1.GetComponent<PlayerPro>();
                if (proScript != null) proScript.TeleportToNextLevel(-50);
            }

            if (player2 != null)
            {

                PlayerAlec player2Script = player2.GetComponent<PlayerAlec>();
                if (player2Script != null) player2Script.TeleportToNextLevel(-50);
            }
        }
        canMove = true;
        freezeTimer = 0f;
        Debug.Log("Players unfrozen and transition complete!");

        
        yield break;
    }
}


