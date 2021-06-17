using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public string nameText;
    public int itemId;
    public Inventory inventory;


    public string NameText{
        get{
            return nameText;
        }
    }

    public void DoActivate()
    {
        inventory.GiveItem(itemId);
        Destroy(gameObject);
    }
}
