using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class SkillDisplay : MonoBehaviour {

    // Get Scriptable Object for Skill
    public Skills skill;

    // Get UI Components
    public TextMeshProUGUI sName;
    public TextMeshProUGUI sLevel;
    public TextMeshProUGUI sPoints;

    public Image sIcon;

    [SerializeField]
    private GodStats m_GodHandler;
    public SkillCheck pickupSkill;
    public SkillCheck arrowSkill;
    public SkillCheck healthSkill;

    public SkillCheck buffSkill;

	// Use this for initialization
	void Start () {

        pickupSkill = GameObject.Find("Pickup Skill").GetComponent<SkillCheck>();
        arrowSkill = GameObject.Find("Arrow Skill").GetComponent<SkillCheck>();
        healthSkill = GameObject.Find("Health Skill").GetComponent<SkillCheck>();
        buffSkill = GameObject.Find("Buff Skill").GetComponent<SkillCheck>();

        m_GodHandler = this.GetComponentInParent<GodHandler>().God;

        // Listener for the power change
        m_GodHandler.onPowerChange += ReactToChange;

        // Listener for level change
        m_GodHandler.onLevelChange += ReactToChange;
        
        // Listener for skill point change;
        m_GodHandler.onPointsChange += ReactToChange;

        if (skill)
            skill.SetValues(this.gameObject, m_GodHandler);

        EnableSkills();
	}

    public void EnableSkills()
    {
        // If the player has the skill already, then show it as enabled
        if (m_GodHandler && skill && skill.EnableSkill(m_GodHandler))
        {
            TurnOnSkillIcon();
        }

        // If the player doesn't have the skill, but can get it, make it interactable
        else if (m_GodHandler && skill && skill.CheckSkills(m_GodHandler))
        {
            this.GetComponent<Button>().interactable = true;
            this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(false);
        }

        else if (m_GodHandler && skill && !skill.CheckSkills(m_GodHandler))
        {
            TurnOffSkillIcon();
        }
    }

    private void OnEnable()
    {
        EnableSkills();
    }

    public void GetSkill()
    {
        if (skill.GetSkill(m_GodHandler))
        {
            TurnOnSkillIcon();
            if (skill.name == "Pickup Objects")
            {
                pickupSkill.skillActive = true;
            }

            if (skill.name == "Arrow Grab")
            {
                arrowSkill.skillActive = true;
            }

            if (skill.name == "Health Swap")
            {
                healthSkill.skillActive = true;
            }

            if (skill.name == "Buff Steal")
            {
                buffSkill.skillActive = true;
            }
        }
    }

    // Turn on the Skill Icon, stop it from being clickable & disable the UI elements that make it change colour
    private void TurnOnSkillIcon()
    {
        this.GetComponent<Button>().interactable = false;
        this.transform.Find("IconParent").Find("Available").gameObject.SetActive(false);
        this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(false);
    }

    private void TurnOffSkillIcon()
    {
        this.GetComponent<Button>().interactable = false;
        this.transform.Find("IconParent").Find("Available").gameObject.SetActive(true);
        this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        m_GodHandler.onPointsChange -= ReactToChange;
    }

    // Update is called once per frame
    void Update () {
		
	}

    void ReactToChange()
    {
        EnableSkills();
    }
}
