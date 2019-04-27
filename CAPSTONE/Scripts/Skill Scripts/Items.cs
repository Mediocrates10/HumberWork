using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "New Item/God/Item")]

public class Items : ScriptableObject {

    public Sprite icon;
    public int goldNeeded;
    public int ID;

    public void SetValues(GameObject ItemDisplayObject, GodStats God)
    {
        if (God)
            CheckItems(God);

        if (ItemDisplayObject)
        {
            ItemDisplay ID = ItemDisplayObject.GetComponent<ItemDisplay>();
            if (ID.iName)
                ID.iName.text = name;
            if (ID.iIcon)
                ID.iIcon.sprite = icon;
            if (ID.iGold)
                ID.iGold.text = goldNeeded.ToString() + "g";
            if (God.gold)
                God.gold.text = "Gold: " + God.GodGold.ToString();
        }
    }

    // Check if the player is able to get the item
    public bool CheckItems(GodStats God)
    {
        // Check if god has enough gold
        if (God.GodGold < goldNeeded)
            return false;

        return true;
    }

    // Check if player already has item
    public bool EnableItem(GodStats God)
    {
        // go through all the items that the god currently has
        List<Items>.Enumerator items = God.Inventory.GetEnumerator();
        while (items.MoveNext())
        {
            var currItem = items.Current;
            if (currItem.name == this.name)
                return true;
        }

        return false;
    }

    // Get new item
    public bool GetItem(GodStats God)
    {
        God.GodGold -= goldNeeded;
        God.gold.text = "Gold: " + God.GodGold.ToString();
        God.Inventory.Add(this);
        return true;
    }
}
