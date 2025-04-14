using UnityEngine;

public class FPSController : MonoBehaviour
{
    // Movement variables
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private float moveX, moveZ;
    private Vector3 moveDirection;
    private CharacterController controller;

    // Camera variables
    public Camera playerCamera;
    public float lookSensitivity = 2f;
    private float xRotation = 0f;

    // Shooting variables
    public GameObject bulletPrefab;    // Assign your bullet prefab here
    public Transform firePoint;        // Where the bullet spawns (like gun barrel tip)
    public float bulletSpeed = 20f;    // Speed of the bullet
    public float fireRate = 0.1f;      // Time between shots
    private float nextFireTime;

    // Gravity and jumping
    private float verticalVelocity;
    private float gravity = -9.81f;
    private bool isGrounded;

    public bool canShoot;
    public GameObject gun;

    void Start()
    {
        canShoot = false;
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMovement();
        HandleCameraRotation();
        
        if(canShoot)
        HandleShooting();


        if (canShoot)
        {
            //gun.GetComponent<MeshRenderer>().enabled = true;
            gun.SetActive(true);
        }
        else
        {
            //gun.GetComponent<MeshRenderer>().enabled = false;
            gun.SetActive(false);
        }
    }

    void HandleMovement()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");

        moveDirection = transform.right * moveX + transform.forward * moveZ;
        moveDirection = moveDirection.normalized * moveSpeed;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            verticalVelocity = jumpForce;
        }

        verticalVelocity += gravity * Time.deltaTime;
        moveDirection.y = verticalVelocity;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleShooting()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate bullet at fire point position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
        // Get the Rigidbody component of the bullet and add force
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Apply velocity in the direction the camera is facing
            rb.linearVelocity = playerCamera.transform.forward * bulletSpeed;
        }
        
        // Optional: Destroy bullet after 5 seconds to prevent clutter
        Destroy(bullet, 5f);
    }

    public bool AllowShooting()
    {
        return canShoot = true;
    }
}