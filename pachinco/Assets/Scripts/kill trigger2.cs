using Unity.VisualScripting;
using UnityEngine;

public class killtrigger3 : MonoBehaviour
{
    // requires at least one object to have a rigid body
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            Destroy(collision.gameObject);
            scoreManager.Instance.AddPoints(5);
        }

    }
}
