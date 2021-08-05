using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoot : MonoBehaviour
{
    public List<UIItem> uIItems = new List<UIItem>();
    public GameObject slotPrefab;
    public Transform slotPanel;
    public LootObject lootObject;
    public int numberOfSlots = 5;
    public List<GameObject> slots = new List<GameObject>();
    // public int itemsDropped;

    void Awake(){
        for (int i = 0; i < numberOfSlots; i++){
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            uIItems.Add(instance.GetComponentInChildren<UIItem>());
            slots.Add(instance);
            // instance.SetActive(false);
        }
        DisableSlots();
        gameObject.SetActive(false);
    }

    public void DisableSlots(){
        foreach (var item in slots)
        {
            item.SetActive(false);
        }
    }

    public void DetermineSlots(int itemsDropped){
        DisableSlots();
        for (int i = 0; i < itemsDropped; i++)
        {
            uIItems[i].transform.parent.gameObject.SetActive(true);
            uIItems[i].gameObject.tag = "Loot";
        }
    }
    
    public void UpdateSlot(int slot, Item item){
        uIItems[slot].UpdateItem(item);
    }
    public void AddNewItem(Item item){
        UpdateSlot(uIItems.FindIndex(i => i.item == null), item);
    }
}
