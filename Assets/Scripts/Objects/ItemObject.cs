using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public string nameText;
    public int itemId;
    public Inventory inventory;
    public bool swinging;
    public Item item;
    public ItemDatabase database;
    public List<KeyValuePair<string, int>> stats = new List<KeyValuePair<string, int>>();

    void Awake(){
        database = GameObject.Find("ItemDatabase").GetComponent<ItemDatabase>();
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        item = database.GetItem(itemId);
        
        foreach (var stat in item.stats){
            KeyValuePair<string, int> statToAdd = stat;
            stats.Add(statToAdd);
            Debug.Log(statToAdd.Key.ToString() + ": " + statToAdd.Value);
        }
    }

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
