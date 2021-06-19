using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEquipment : MonoBehaviour
{
    public List<UIItem> uIItems = new List<UIItem>();
    public GameObject slotPrefab;
    public Transform slotPanel;
    public int numberOfSlots = 3;
    public UIInventory uIInventory;
    // public Inventory inventory;

    private void Awake(){
        // inventory = GameObject.Find("Player").GetComponent<Inventory>();
        uIInventory = GameObject.Find("InventoryPanel").GetComponent<UIInventory>();
        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            uIItems.Add(instance.GetComponentInChildren<UIItem>());
        }
    }
    public void UpdateSlot(int slot, Item item){
        uIItems[slot].UpdateItem(item);
        uIItems[slot].equipped = true;
    }
    public void AddNewItem(Item item){
        UpdateSlot(uIItems.FindIndex(i => i.item == null), item);
    }
    public void RemoveItem(Item item){
        UpdateSlot(uIItems.FindIndex(i => i.item == item), null);
    }

    public void UnequipItem(Item item){
        uIInventory.AddNewItem(item);
        RemoveItem(item);
    }
}
