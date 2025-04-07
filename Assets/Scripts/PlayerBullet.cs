using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target")) 
        {
            ScoreController.instance.IncreaseScore();
            Destroy(gameObject); 
        }
    }
}
