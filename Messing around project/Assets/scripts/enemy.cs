using UnityEngine;

public class enemy : MonoBehaviour
{

    public string playerTag = "Player";
    [SerializeField]
    private float speed = 5f;
   
   
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // 1. Get continuous left/right input (-1 for A, 1 for D, 0 for nothing)

        Vector3 moveDir = Vector3.zero;


            moveDir.x = -speed;

        
        if (Input.GetKey(KeyCode.D))
        {
            moveDir.x = speed;

        }
        transform.position += moveDir * Time.deltaTime;



    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag(playerTag))
            {
                Destroy(collision.gameObject);
            }
        }

    }

}
