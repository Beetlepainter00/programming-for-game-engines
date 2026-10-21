using UnityEngine;

public class speen: MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Spin Settings")]
    [Tooltip("Degrees per second around the X, Y, and Z axes")]
    public Vector3 rotationSpeed = new Vector3(0f, 100f, 0f); // Default: spins on the Y-axis

    void Update()
    {
        // Multiply by Time.deltaTime to keep the spin smooth and frame-rate independent
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
