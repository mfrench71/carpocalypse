using UnityEngine;

  public class VehicleController : MonoBehaviour
  {
      [Header("Movement Settings")]
      public float moveSpeed = 10f;
      public float rotationSpeed = 200f;

      [Header("Shooting Settings")]
      public GameObject bulletPrefab;
      public Transform firePoint;
      public float fireRate = 0.3f;

      private Rigidbody rb;
      private float moveInput;
      private float turnInput;
      private float nextFireTime = 0f;

      void Start()
      {
          rb = GetComponent<Rigidbody>();

          // Create fire point if it doesn't exist
          if (firePoint == null)
          {
              GameObject fp = new GameObject("FirePoint");
              fp.transform.parent = transform;
              fp.transform.localPosition = new Vector3(0f, 0f, 1f);
              firePoint = fp.transform;
          }
      }

      void Update()
      {
          // Get input from keyboard
          moveInput = Input.GetAxis("Vertical");   // W/S or Up/Down arrows
          turnInput = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows

          // Shooting
          if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
          {
              Shoot();
              nextFireTime = Time.time + fireRate;
          }
      }

      void FixedUpdate()
      {
          // Move the vehicle forward/backward
          Vector3 movement = transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime;
          rb.MovePosition(rb.position + movement);

          // Rotate the vehicle left/right
          float rotation = turnInput * rotationSpeed * Time.fixedDeltaTime;
          Quaternion turnRotation = Quaternion.Euler(0f, rotation, 0f);
          rb.MoveRotation(rb.rotation * turnRotation);
      }

      void Shoot()
      {
          if (firePoint != null && ObjectPool.Instance != null)
          {
              ObjectPool.Instance.SpawnFromPool("Bullet", firePoint.position, firePoint.rotation);
          }
      }
  }