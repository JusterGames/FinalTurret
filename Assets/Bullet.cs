using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLifetime = 3f;
    public float bulletSpeed = 20f;
    public LayerMask collisionLayer;

    private void OnEnable()
    {
        // Set a timer to disable the bullet after a certain lifetime
        Invoke("DisableBullet", bulletLifetime);
    }

    void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    void DisableBullet()
    {
        // Disable the bullet after its lifetime expires
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Handle collision with other objects
        if ((collisionLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            // Disable the bullet on collision
            gameObject.SetActive(false);

            // You can add additional logic here, such as damaging the player/enemy
        }
    }
}
