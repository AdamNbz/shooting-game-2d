using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    public string skillName;

    public BaseSkill(string skillName_)
    {
        this.skillName = skillName_;
    }
    public virtual void ActiveSkill()
    {
        Debug.Log("Skill activated: " + skillName);
    }
}
