using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBulletSkill : BaseSkill
{
    [SerializeField] private GameObject bulletPrefab;
    public float rotationSpeed = 100f;
    private bool isActive = false;
    public RotatingBulletSkill() : base("RotatingBullet") { }

    public override void ActiveSkill()
    {
        PlayerController player = SkillManager.instance.player;

        Vector3 spawnPosition = player.transform.position + new Vector3(1f, 0, 0);
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        
        StartCoroutine(RotateBullet(bullet, player));
        Debug.Log("RotatingBullet activated: " + skillName);
    }

    private IEnumerator RotateBullet(GameObject bullet, PlayerController player)
    {
        isActive = true;
        while (isActive)
        {
            bullet.transform.RotateAround(player.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
