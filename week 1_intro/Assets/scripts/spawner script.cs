using Unity.VisualScripting;
using UnityEngine;

public class spawnerscript : MonoBehaviour
{

    // Update is called once per frame
   [SerializeField]
    private GameObject dropitem = null;
    [SerializeField]
    private float moveSpeed = 5;
    void Update()
    {

        //replace with GetKey for nonsense
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Spawner Update");
            Instantiate(dropitem, transform.position, Quaternion.identity);
        }
        Vector3 moveDir = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            moveDir.y = moveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDir.x = -moveSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDir.y = -moveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDir.x = moveSpeed;
        }
        transform.position += moveDir * Time.deltaTime;
    }
}
