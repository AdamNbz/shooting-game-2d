using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultishotSkill : BaseSkill
{
    private int bulletCount = 3;
    public MultishotSkill() : base("Multishot") { }

    public override void ActiveSkill()
    {
        PlayerController player = SkillManager.instance.player;
        if (player)
        {
            for (int i=0; i<bulletCount; i++)
            {
                player.Shoot();
            }
        }
        Debug.Log("Multishot activated: " + skillName);
    }
}
