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
    // public int itemsDropped;

    void Awake(){
        for (int i = 0; i < numberOfSlots; i++){
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            uIItems.Add(instance.GetComponentInChildren<UIItem>());
            instance.SetActive(false);
        }
        // for (int i = 0; i < numberOfSlots; i++)
        // {
        //     Debug.Log("disabled");
        //     uIItems[i].gameObject.GetComponentInParent<GameObject>().gameObject.SetActive(false);
        // }
        gameObject.SetActive(false);
    }

    // void Awake(){
    //     itemsDropped = lootObject.itemsToDrop;
        
    // }

    public void DetermineSlots(int itemsDropped){
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
