using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float minZ = -4f;
    public float maxZ = 24f;

    private float targetZ;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        SetRandomTargetZ();
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = new Vector3(startPosition.x, startPosition.y, targetZ);

        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);

        if (Mathf.Abs(currentPosition.z - targetZ) < 0.05f)
        {
            SetRandomTargetZ();
        }
    }

    void SetRandomTargetZ()
    {
        targetZ = Random.Range(minZ, maxZ);
    }
}
