using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot 
{
    public int id;
    public string slotName;
    public bool filled;
    public Item item;

    public Slot(int id, string slotName, bool filled, Item item){
        this.id = id;
        this.slotName = slotName;
        this.filled = filled;
        this.item = item;
    }

    public Slot(Slot slot){
        this.id = slot.id;
        this.slotName = slot.slotName;
        this.filled = slot.filled;
        this.item = slot.item;
    }
}
