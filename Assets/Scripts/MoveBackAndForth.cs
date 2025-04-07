using UnityEngine;

public class MoveBackAndForth : MonoBehaviour
{
    public float speed = 2.0f;    // Скорость движения
    public float distance = 3.0f; // Расстояние движения в каждую сторону
    public bool moveOnX = true;   // Движение по оси X
    public bool moveOnY = false;  // Движение по оси Y
    public bool moveOnZ = false;  // Движение по оси Z
    public bool reverseDirection = false; // Направление: false - слева направо, true - справа налево
    
    private Vector3 startPosition;
    private float time;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        time += Time.deltaTime * speed;
        float position = Mathf.PingPong(time, distance);
        
        // Инвертируем направление, если reverseDirection = true
        if (reverseDirection)
            position = -position;
        
        Vector3 movement = Vector3.zero;
        
        if (moveOnX)
            movement.x = position;
        if (moveOnY)
            movement.y = position;
        if (moveOnZ)
            movement.z = position;
        
        transform.position = startPosition + movement;
    }
}