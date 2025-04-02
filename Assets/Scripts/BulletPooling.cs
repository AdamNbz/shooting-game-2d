using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class BulletPooling : MonoBehaviour
{
    #region Singleton

    public static BulletPooling instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion
    public PlayerBullet playerBulletPrefab;
    public EnemyBullet enemyBulletPrefab;
    private Queue<PlayerBullet> playerBulletPool = new Queue<PlayerBullet>();
    private Queue<EnemyBullet> enemyBulletPool = new Queue<EnemyBullet>();
    public PlayerBullet GetPlayerBullet()
    {
        if (playerBulletPool.Count <= 0)
        {
            int count = 10;
            for (int i = 0; i < count; i++)
            {
                var New_pBullet = Instantiate(playerBulletPrefab);
                New_pBullet.gameObject.SetActive(false);
                playerBulletPool.Enqueue(New_pBullet);
            }
        }
        var pBullet = playerBulletPool.Dequeue();
        pBullet.gameObject.SetActive(true);
        return pBullet;
    }

    public EnemyBullet GetEnemyBullet()
    {
        if (enemyBulletPool.Count <= 0)
        {
            int count = 6;
            for (int i = 0; i < count; i++)
            {
                var New_eBullet = Instantiate(enemyBulletPrefab);
                New_eBullet.gameObject.SetActive(false);
                enemyBulletPool.Enqueue(New_eBullet);
            }
        }
        var eBullet = enemyBulletPool.Dequeue();
        eBullet.gameObject.SetActive(true);
        return eBullet;
    }

    public void ReturnPlayerBullet(PlayerBullet bullet)
    {
        bullet.gameObject.SetActive(false);
        playerBulletPool.Enqueue(bullet);
    }
    public void ReturnEnemyBullet(EnemyBullet bullet)
    {
        bullet.gameObject.SetActive(false);
        enemyBulletPool.Enqueue(bullet);
    }
}
