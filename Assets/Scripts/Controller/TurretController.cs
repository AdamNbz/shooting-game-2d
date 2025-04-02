using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class TurretController : BaseEnemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireInterval = 2.5f;
    private float fireTimer = 0f;
    
    void Start()
    {
        HP = 50f;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;    
    }

    void Update()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (!playerObj) return;

        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            Shoot();
            fireTimer = 0f;
        }
    }
    private void Shoot()
    {
        for (float angle = 0f; angle < 360f; angle += 60f)
        {
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
            
            EnemyBullet bullet = BulletPooling.instance.GetEnemyBullet();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.SetDirection(direction);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            HP -= 2;
            if (HP <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    void OnDestroy()
    {
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        if (spawner) spawner.OnTurretDestroyed();
    }
}
