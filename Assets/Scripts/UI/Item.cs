using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public int id;
    public string title;
    public string description;
    public Sprite icon;
    public Dictionary<string, int> stats = new Dictionary<string, int>();
    public bool equipable;
    public string slot;

    public Item(int id, string title, string description, bool equipable, string slot, Dictionary<string, int> stats) {
        this.id = id;
        this.title = title;
        this.description = description;
        this.icon = Resources.Load<Sprite>("Sprites/Items/" + title);
        this.stats = stats;
        this.equipable = equipable;
        this.slot = slot;
    }

    public Item(Item item){
        this.id = item.id;
        this.title = item.title;
        this.description = item.description;
        this.icon = Resources.Load<Sprite>("Sprites/Items/" + item.title);
        this.stats = item.stats;
        this.equipable = item.equipable;
        this.slot = item.slot;
    }
}


