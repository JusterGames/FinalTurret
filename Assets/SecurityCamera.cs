using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public float detectionRange = 10f;
    public LayerMask playerLayer;

    void Update()
    {
        // Find the player GameObject based on the "Player" layer
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            // Look at the player
            transform.LookAt(player.transform);

            // Cast a ray from the camera's position forward
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            // Check if the ray hits something within the detection range and on the player layer
            if (Physics.Raycast(ray, out hit, detectionRange, playerLayer))
            {
                // Player is detected
                Debug.Log("Player Detected!");

                // You can add more actions here, like triggering an alarm or capturing the player
            }
        }
    }
}
