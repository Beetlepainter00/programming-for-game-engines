using UnityEngine;

public class ZoneForce : MonoBehaviour
{
    public Vector2 pushDirection = Vector2.up;
    public float pushForce = 10f;

    // Runs every physics frame an object stays inside the trigger area
    private void OnTriggerStay2D(Collider2D other)
    {
        // Check if the object entering has a Rigidbody2D to push
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Applies a continuous force to the other object
            rb.AddForce(pushDirection.normalized * pushForce);
        }
    }
}

