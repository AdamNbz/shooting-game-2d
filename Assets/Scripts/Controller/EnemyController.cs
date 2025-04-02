using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseEnemy
{
    private float speed = 5f;
    private int damage = 10;
    private Transform playerTransform;

    void Start()
    {
        HP = 20f;
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj)
            playerTransform = playerObj.transform;
    }
    void Update()
    {
        if (playerTransform)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position = CheckEnemyPosWithCamera(direction);

            if (direction.magnitude > 0.1f)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
            }
        }
    }
    private Vector3 CheckEnemyPosWithCamera(Vector3 direction)
    {
        Vector3 movementUpdate = transform.position + direction * speed * Time.deltaTime;

        Vector2 cameraBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        movementUpdate.x = Mathf.Clamp(movementUpdate.x, -cameraBound.x, cameraBound.x);
        movementUpdate.y = Mathf.Clamp(movementUpdate.y, -cameraBound.y, cameraBound.y);

        return movementUpdate;
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
        else if (other.gameObject.CompareTag("Player"))
        {
            PlayerController pc = other.gameObject.GetComponent<PlayerController>();
            if (pc)
            {
                pc.TakeDamage(damage);
            }
        }
    }

    private void OnDestroy()
    {
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        if (spawner) spawner.OnEnemyKilled();
    }
}
