using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillTree
{
    public Skills skill;
    public bool active;

    public SkillTree(Skills skill, bool active)
    {
        this.skill = skill;
        this.active = active;
    }
}
