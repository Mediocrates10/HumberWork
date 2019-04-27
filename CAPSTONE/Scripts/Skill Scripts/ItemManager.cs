using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public Items item;
    public bool active;

    public Inventory(Items item, bool active)
    {
        this.item = item;
        this.active = active;
    }
}
