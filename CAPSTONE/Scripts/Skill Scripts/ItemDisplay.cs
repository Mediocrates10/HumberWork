using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ItemDisplay : MonoBehaviour {

    // Get Scriptable Object for Item
    public Items item;

    // Get UI Components;
    public TextMeshProUGUI iName;
    public TextMeshProUGUI iGold;

    public Image iIcon;

    [SerializeField]
    private GodStats m_GodHandler;
    public ItemCheck healthPotion;
    public ItemCheck enragePotion;

    // Use this for initialization
    void Start () {

        healthPotion = GameObject.Find("Health Potion").GetComponent<ItemCheck>();
        enragePotion = GameObject.Find("Enrage Potion").GetComponent<ItemCheck>();

        m_GodHandler = this.GetComponentInParent<GodHandler>().God;

        // Listener for the gold change
        m_GodHandler.onGoldChange += ReactToChange;

        if (item)
            item.SetValues(this.gameObject, m_GodHandler);

        EnableItems();
	}

    public void EnableItems()
    {
        // If the player has the item already, then show it as enabled
        if (m_GodHandler && item && item.EnableItem(m_GodHandler))
        {
            TurnOnItemIcon();
        }

        // If the player doesn't have the item, but can get it, make it interactable
        else if (m_GodHandler && item && item.CheckItems(m_GodHandler))
        {
            this.GetComponent<Button>().interactable = true;
            this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(false);
        }

        else if (m_GodHandler && item && !item.CheckItems(m_GodHandler))
        {
            TurnOffItemIcon();
        }
    }

    private void OnEnable()
    {
        EnableItems();
    }

    public void GetItem()
    {
        if (item.GetItem(m_GodHandler))
        {
            TurnOnItemIcon();
            if (item.name == "Health Potion")
            {
                healthPotion.itemActive = true;
            }

            if (item.name == "Enrage Potion")
            {
                enragePotion.itemActive = true;
            }
        }
    }

    private void TurnOnItemIcon()
    {
        this.GetComponent<Button>().interactable = false;
        this.transform.Find("IconParent").Find("Available").gameObject.SetActive(false);
        this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(false);
    }

    private void TurnOffItemIcon()
    {
        this.GetComponent<Button>().interactable = false;
        this.transform.Find("IconParent").Find("Available").gameObject.SetActive(true);
        this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        m_GodHandler.onGoldChange -= ReactToChange;
    }

    void ReactToChange()
    {
        EnableItems();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
