using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class DodgeAgent : Agent
{
    public float moveSpeed = 5f;
    public float dodgeCooldown = 1.0f;
    private float lastDodgeTime = 0f;

    public float bulletDetectionRange = 10f;
    public LayerMask bulletLayer;
    public LayerMask wallLayer;

    private Rigidbody rb;
    private float leftWallDist;
    private float rightWallDist;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();

        // Убедимся, что Rigidbody настроен правильно
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
    }

    public override void OnEpisodeBegin()
    {
        // Сброс позиции и скорости
        transform.localPosition = new Vector3(0f, transform.localPosition.y, transform.localPosition.z);
        rb.linearVelocity = Vector3.zero;
        lastDodgeTime = 0f;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        leftWallDist = GetWallDistance(Vector3.left);
        rightWallDist = GetWallDistance(Vector3.right);

        Vector3 closestBullet = FindClosestBullet();
        Vector3 relativeBulletPos = closestBullet == Vector3.zero ? Vector3.zero : closestBullet - transform.position;

        // Предотвращение ошибок из-за NaN/Infinity
        relativeBulletPos = ClampVector(relativeBulletPos);

        sensor.AddObservation(Mathf.Clamp(leftWallDist, 0f, 20f)); // 1
        sensor.AddObservation(Mathf.Clamp(rightWallDist, 0f, 20f)); // 2
        sensor.AddObservation(relativeBulletPos.normalized);        // 3, 4
        sensor.AddObservation(Mathf.Clamp01(relativeBulletPos.magnitude / bulletDetectionRange)); // 5

        sensor.AddObservation(Time.time - lastDodgeTime < dodgeCooldown ? 1f : 0f); // 6
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (Time.time < lastDodgeTime + dodgeCooldown)
            return;

        int moveDir = actions.DiscreteActions[0]; // 0: none, 1: left, 2: right
        Vector3 dir = Vector3.zero;

        if (moveDir == 1 && leftWallDist > 0.5f) dir = Vector3.left;
        else if (moveDir == 2 && rightWallDist > 0.5f) dir = Vector3.right;

        if (dir != Vector3.zero)
        {
            Vector3 targetPos = rb.position + dir * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(targetPos);
            lastDodgeTime = Time.time;
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discrete = actionsOut.DiscreteActions;
        discrete[0] = 0;
        if (Input.GetKey(KeyCode.A)) discrete[0] = 1;
        else if (Input.GetKey(KeyCode.D)) discrete[0] = 2;
    }

    Vector3 FindClosestBullet()
    {
        Collider[] bullets = Physics.OverlapSphere(transform.position, bulletDetectionRange, bulletLayer);
        Vector3 closest = Vector3.zero;
        float closestDist = bulletDetectionRange;

        foreach (Collider bullet in bullets)
        {
            float dist = Vector3.Distance(transform.position, bullet.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = bullet.transform.position;
            }
        }

        return closest;
    }

    float GetWallDistance(Vector3 direction)
    {
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, 10f, wallLayer))
            return hit.distance;

        return 10f;
    }

    Vector3 ClampVector(Vector3 v)
    {
        if (float.IsNaN(v.x) || float.IsInfinity(v.x)) v.x = 0f;
        if (float.IsNaN(v.y) || float.IsInfinity(v.y)) v.y = 0f;
        if (float.IsNaN(v.z) || float.IsInfinity(v.z)) v.z = 0f;
        return v;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, bulletDetectionRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, Vector3.left * leftWallDist);
        Gizmos.DrawRay(transform.position, Vector3.right * rightWallDist);
    }
}
