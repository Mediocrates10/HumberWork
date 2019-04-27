using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingSystem : MonoBehaviour {

    public int XP;
    public int currentLvl;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateXP(5);
	}

    public void UpdateXP(int xp)
    {
        XP += xp;

        int curLvl = (int)(0.1f * Mathf.Sqrt(XP));

        if(curLvl != currentLvl)
        {
            currentLvl = curLvl;
        }

        int xpNextLevel = 100 *  (currentLvl + 1) * (currentLvl + 1);
        int differenceXp = xpNextLevel - XP;

        int totalDifference = xpNextLevel - (100 * currentLvl * currentLvl);

    }
}
