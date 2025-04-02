using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private float maxSpeed = 10f;
    private float acceleration = 15f;
    private float friction = 15f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 inputDirection = Vector3.zero;
    [SerializeField] private int health = 100;
    [SerializeField] private GameObject bulletPrefab;

    void Update()
    {
        Shoot();
    }

    void FixedUpdate()
    {
        Movement();
    }
    
    void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        inputDirection = new Vector3(horizontal, vertical, 0).normalized;
        
        if (inputDirection.magnitude > 0)
        {
            velocity += inputDirection * acceleration * Time.deltaTime;
            if (velocity.magnitude > maxSpeed) 
                velocity = velocity.normalized * maxSpeed;
        }
        else
        {
            if (velocity.magnitude > 0)
            {
                float deceleration = friction * Time.deltaTime;
                if (velocity.magnitude <= deceleration)
                    velocity = Vector3.zero;
                else
                    velocity -= velocity.normalized * deceleration;
            }
        }

        transform.position = CheckPlayerPosWithCamera(velocity);

        if (velocity.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }
    }

    private Vector3 CheckPlayerPosWithCamera(Vector3 velocity)
    {
        Vector3 movementUpdate = transform.position + velocity * Time.deltaTime;
        
        Vector2 cameraBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        movementUpdate.x = Mathf.Clamp(movementUpdate.x, -cameraBound.x, cameraBound.x);
        movementUpdate.y = Mathf.Clamp(movementUpdate.y, -cameraBound.y, cameraBound.y);

        return movementUpdate;
    }

    public void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
            List<GameObject> allEnemies = new List<GameObject>(enemies);
            allEnemies.AddRange(turrets);
            Vector3 shootDirection;
            if (allEnemies.Count > 0)
            {
                GameObject targetEnemy = allEnemies[Random.Range(0, enemies.Length)];
                shootDirection = (targetEnemy.transform.position - transform.position).normalized;
            }
            else
            {
                shootDirection = transform.up;
            }
            PlayerBullet bullet = BulletPooling.instance.GetPlayerBullet();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.SetDirection(shootDirection);
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        Debug.Log("Player health: " + health);
        if (health <= 0)
        {
            Debug.Log("Game Over.");
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage(3);
        }
    }
}
