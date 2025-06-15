using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;       // Assign in Inspector
    public Transform firePoint;           // Where bullet spawns
    public float bulletSpeed = 10f;
    public float cooldown = 1;
    public float cooldownTime;

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0) && cooldownTime <= 0))
        {
            cooldownTime = cooldown;
            Fire();
        }
        cooldownTime -= Time.deltaTime;
    }

    void Fire()
    {
        
        
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = firePoint.right * bulletSpeed;
        
    }
}
