using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;  
    public Transform shootPoint;     
    public Transform target;         
    public float shootInterval = 1.5f;
    public float bulletSpeed = 10f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(Shoot), 1f, shootInterval);
    }

    

    public void Shoot()
    {
        if (target == null) return;

        // Instantiate bullet
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        
        // Calculate direction
        Vector3 direction = (target.position - shootPoint.position).normalized;
        
        // Apply velocity
        bullet.GetComponent<Rigidbody>().linearVelocity = direction * bulletSpeed;
        
        // Destroy bullet after 3 seconds
        Destroy(bullet, 3f);
    }
}
