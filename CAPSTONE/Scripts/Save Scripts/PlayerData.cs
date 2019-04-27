using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PlayerData {

    public int level;
    public int power;
    public int skillPoints;
    public int gold;
    public int[] skills;
    public int[] items;
    public int sIndex = 0;
    public int iIndex = 0;


    public PlayerData (GodStats god)
    {
        level = god.GodLevel;
        power = god.GodPower;
        gold = god.GodGold;

        skillPoints = god.GodSkillPoints;

        skills = new int[god.GodSkills.Count];
        items = new int[god.Inventory.Count];

        foreach (Skills skill in god.GodSkills)
        {
            switch (skill.ID)
            {
                case 1:
                    skills[sIndex] = 1;
                    sIndex++;
                    break;
                case 2:
                    skills[sIndex] = 2;
                    sIndex++;
                    break;
                case 3:
                    skills[sIndex] = 3;
                    sIndex++;
                    break;
                case 4:
                    skills[sIndex] = 4;
                    sIndex++;
                    break;                           
            }
        }

        foreach (Items item in god.Inventory)
        {
            switch (item.ID)
            {
                case 1:
                    items[iIndex] = 1;
                    iIndex++;
                    break;
                case 2:
                    items[iIndex] = 2;
                    iIndex++;
                    break;
            }
        }
    }

}
