using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; 
    public Vector2 offset;   
    public float lockedYPosition = 0f;

    void LateUpdate()
    {
        if (player != null)
        {
            // Keep the camera's original Z depth so elements stay visible
            transform.position = new Vector3(player.position.x + offset.x, lockedYPosition, transform.position.z);
        }
    }
}
