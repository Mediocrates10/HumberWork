using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GodStats : MonoBehaviour {

    public TextMeshProUGUI gold;

    [Header("God Stats")]
    public string godName;

    [SerializeField]
    private int m_godPower = 0;
    public int GodPower
    {
        get { return m_godPower; }

        set
        {
            m_godPower = value;

            if (onPowerChange != null)
                onPowerChange();
        }
    }

    [SerializeField]
    private int m_godLevel = 0;
    public int GodLevel
    {
        get { return m_godLevel; }

        set
        {
            m_godLevel = value;

            if (onLevelChange != null)
                onLevelChange();
        }
    }

    [SerializeField]
    private int m_godSkillPoints = 0;
    public int GodSkillPoints
    {
        get { return m_godSkillPoints; }

        set
        {
            m_godSkillPoints = value;

            if (onPointsChange != null)
                onPointsChange();
        }
    }

    [SerializeField]
    private int m_godGold = 0;
    public int GodGold
    {
        get { return m_godGold; }

        set
        {
            m_godGold = value;

            if (onGoldChange != null)
                onGoldChange();
        }
    }

    [Header("Skill Bank")]
    public Skills healthSwap;
    public Skills pickupObjects;
    public Skills arrowGrab;
    public Skills buffSwap;

    [Header("Item Bank")]
    public Items healthPotion;
    public Items enragePotion;

    [Header("God Skills")]
    public List<Skills> GodSkills = new List<Skills>();

    [Header("Inventory")]
    public List<Items> Inventory = new List<Items>();

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (GodPower >= 1000)
        {
            LevelUp();            
        }
    }

    // Delegates for listeners

    public delegate void OnPowerChange();
    public event OnPowerChange onPowerChange;

    public delegate void OnLevelChange();
    public event OnLevelChange onLevelChange;

    public delegate void OnPointsChange();
    public event OnPointsChange onPointsChange;

    public delegate void OnGoldChange();
    public event OnGoldChange onGoldChange;

    // testing functions
    public void UpdatePower(int amount)
    {
        GodPower += amount;        
    }

    public void UpdateGold(int amount)
    {
        GodGold += amount;
    }

    public void LevelUp()
    {      
        GodPower = 0;
        GodLevel++;
        GodSkillPoints++;
        GodGold += 200;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        //SkillDisplay sd = new SkillDisplay();

        GodSkills.Clear();

        m_godLevel = data.level;
        m_godPower = data.power;
        m_godSkillPoints = data.skillPoints;
        m_godGold = data.gold;

        foreach (int skill in data.skills)
        {
            switch (skill)
            {
                case 1:
                    healthSwap.GetSkill(this);
                    healthSwap.EnableSkill(this);
                    //sd.healthSkill = GameObject.Find("Health Skill").GetComponent<SkillCheck>();
                    //sd.healthSkill.skillActive = true;
                    break;
                case 2:                   
                    pickupObjects.GetSkill(this);
                    pickupObjects.EnableSkill(this);
                    //sd.pickupSkill = GameObject.Find("Pickup Skill").GetComponent<SkillCheck>();
                    //sd.pickupSkill.skillActive = true;
                    break;
                case 3:
                    arrowGrab.GetSkill(this);
                    arrowGrab.EnableSkill(this);
                    //sd.arrowSkill = GameObject.Find("Arrow Skill").GetComponent<SkillCheck>();
                   // sd.arrowSkill.skillActive = true;
                    break;
                case 4:
                    buffSwap.GetSkill(this);
                    buffSwap.EnableSkill(this);
                    //sd.buffSkill = GameObject.Find("Pickup Skill").GetComponent<SkillCheck>();
                   //sd.buffSkill.skillActive = true;
                    break;
            }
        }

        foreach (int item in data.items)
        {
            switch (item)
            {
                case 1:
                    healthPotion.GetItem(this);
                    healthPotion.GetItem(this);
                    break;
                case 2:
                    enragePotion.GetItem(this);
                    enragePotion.GetItem(this);
                    break;
            }
        }
    }

}
