using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Agent")) 
        {
            Destroy(gameObject); 
        }
    }

}
