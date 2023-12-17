using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform gunTransform;
    public float rotationSpeed = 5f;
    public float detectionRange = 10f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public float bulletSpeed = 20f;
    public bool useRandomizedAim = true;

    private float nextFireTime;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            bool canSeePlayer = CanSeePlayer();

            if (canSeePlayer)
            {
                // If player is in range, face the player
                RotateTurret();

                if (CanShoot())
                {
                    Shoot();
                }
            }
        }
    }

    bool CanSeePlayer()
    {
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, direction, out hit, detectionRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }

        return false;
    }

    void RotateTurret()
    {
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            Quaternion toRotation;

            if (useRandomizedAim)
            {
                // Add some randomness to the aim
                Vector3 randomizedDirection = direction + Random.onUnitSphere * 0.1f;
                toRotation = Quaternion.LookRotation(randomizedDirection);
            }
            else
            {
                toRotation = Quaternion.LookRotation(direction);
            }

            gunTransform.rotation = Quaternion.Slerp(gunTransform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    bool CanShoot()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRange && Time.time > nextFireTime)
        {
            return true;
        }

        return false;
    }

    void Shoot()
    {
        nextFireTime = Time.time + 1f / fireRate;

        // Instantiate bullet using object pooling
        GameObject bullet = ObjectPooler.Instance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = gunTransform.rotation;
            bullet.SetActive(true);

            // Set bullet speed
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.velocity = bullet.transform.forward * bulletSpeed;
            }
        }
    }
}
