using System.Collections;
using TMPro;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBullet : BaseBullet
{
    [SerializeField] private Coroutine lifeTimer;
    private float lifeDuration = 2f;

    protected override void OnEnable()
    {
        base.OnEnable();
        if (lifeTimer != null)
        {
            StopCoroutine(lifeTimer);
        }
        lifeTimer = StartCoroutine(LifeTimer());
    }
    private IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(lifeDuration);
        BulletPooling.instance.ReturnEnemyBullet(this);
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (lifeTimer != null) StopCoroutine(lifeTimer);
            BulletPooling.instance.ReturnEnemyBullet(this);
        }
    }
}
