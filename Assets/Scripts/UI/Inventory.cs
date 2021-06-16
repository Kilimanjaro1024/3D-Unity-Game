using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> characterItems = new List<Item>();
    public ItemDatabase itemDatabase;

    void Start(){
        
        GiveItem(1);
        GiveItem(0);
        GiveItem(2);
        RemoveItem(2);
        RemoveItem(0);
        for (int i = 0; i < characterItems.Count; i++)
        {
            Debug.Log(characterItems[i].title);
            Debug.Log(characterItems[i].id);
        }
    }

    public void GiveItem(int id){
        Item itemToAdd = itemDatabase.GetItem(id);
        characterItems.Add(itemToAdd);
        Debug.Log("Added item: " + itemToAdd.title);
    }

    public void GiveItem(string itemName){
        Item itemToAdd = itemDatabase.GetItem(itemName);
        characterItems.Add(itemToAdd);
        Debug.Log("Added item: " + itemToAdd.title);
    }

      public Item CheckForItem(int id)
    {
        return characterItems.Find(item => item.id == id);
    }

    public void RemoveItem(int id)
    {
        Item itemToRemove = CheckForItem(id);
        if (itemToRemove != null)
        {
            characterItems.Remove(itemToRemove);
            
            Debug.Log("Removed item: " + itemToRemove.title);
        }
    }
}
