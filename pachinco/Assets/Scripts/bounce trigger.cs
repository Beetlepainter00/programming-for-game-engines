using UnityEngine;
using UnityEngine.SceneManagement;

public class BallBounceTracker : MonoBehaviour
{
    [Tooltip("Points awarded per bounce")]
    public int pointsPerBounce = 10;

    // Use OnCollisionEnter2D if your game is built in 2D mode
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object we bounced off is tagged as "Bouncable"
        if (collision.gameObject.CompareTag("Bouncable"))
        {
            // Call the Score Manager to add points
            if (scoreManager.Instance != null)
            {
                scoreManager.Instance.AddPoints(pointsPerBounce);
                
            }
        }
    }
}