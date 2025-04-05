using System.Collections;
using TMPro;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBullet : BaseBullet
{
    [SerializeField] private Coroutine lifeTimer;
    private float lifeDuration = 2f;

    private IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(lifeDuration);
        BulletPooling.instance.ReturnPlayerBullet(this);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        if (lifeTimer != null)
        {
            StopCoroutine(lifeTimer);
        }
        lifeTimer = StartCoroutine(LifeTimer());
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Turret"))
        {
            if (lifeTimer != null) StopCoroutine(lifeTimer);
            BulletPooling.instance.ReturnPlayerBullet(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Vector2 normal = other.contacts[0].normal;
            rb.velocity = Vector2.Reflect(rb.velocity, normal);
        }
    }
}
