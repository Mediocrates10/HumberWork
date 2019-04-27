using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Skill/God/Skill")]

public class Skills : ScriptableObject
{
    public Sprite icon;
    public int levelNeeded;
    public int pointsNeeded;
    public int ID;

    public void SetValues(GameObject SkillDisplayObject, GodStats God)
    {
        if (God)
            CheckSkills(God);

        if (SkillDisplayObject)
        {
            SkillDisplay SD = SkillDisplayObject.GetComponent<SkillDisplay>();
            if (SD.sName)
                SD.sName.text = name;
            if (SD.sIcon)
                SD.sIcon.sprite = icon;
            if (SD.sLevel)
                SD.sLevel.text = levelNeeded.ToString();
            if (SD.sPoints)
                SD.sPoints.text = pointsNeeded.ToString() + " SP";
        }

    }

    // Check if the player is able to get the skill
    public bool CheckSkills(GodStats God)
    {
        // Check if god is right level
        if (God.GodLevel < levelNeeded)
            return false;
        // Check if god has enough skill points
        if (God.GodSkillPoints < pointsNeeded)
            return false;

        return true;
    }

    // Check if player already has skill
    public bool EnableSkill(GodStats God)
    {
        // go through all the skills that the god currently has;
        List<Skills>.Enumerator skills = God.GodSkills.GetEnumerator();
        while (skills.MoveNext())
        {
            var currSkill = skills.Current;
            if (currSkill.name == this.name)
                return true;
        }

        return false;
    }

    // Get new skill
    public bool GetSkill(GodStats God)
    {
        God.GodSkillPoints -= pointsNeeded;
        God.GodSkills.Add(this);
        return true;
    }

}
