using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public PlayerController player;
    private Dictionary<string, BaseSkill> skills = new Dictionary<string, BaseSkill>();

    #region Singleton
    public static SkillManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    private void Start()
    {
        AddSkill(new MultishotSkill());
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) ActivateSkill("Multishot");
    }
    public void AddSkill(BaseSkill skill)
    {
        if (!skills.ContainsKey(skill.skillName))
        {
            skills.Add(skill.skillName, skill);
        }
    }
    public void ActivateSkill(string skillName)
    {
        if (skills.ContainsKey(skillName))
        {
            skills[skillName].ActiveSkill();
        }
    }
}
