using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> characterItems = new List<Item>();
    public ItemDatabase itemDatabase;
    public UIInventory inventoryUI;
    public UIEquipment equipmentUI;
    public CharacterController controller;

    void Start(){
        controller = gameObject.GetComponent<CharacterController>();
        GiveItem(1);
        GiveItem(0);
        GiveItem(2);
        inventoryUI.gameObject.SetActive(false);
    }

    void Update(){
        UIController();
    }

    public void UIController(){
        if (Input.GetKeyDown(KeyCode.I)){
            controller.m_Pause = !controller.m_Pause;
            inventoryUI.gameObject.SetActive(!inventoryUI.gameObject.activeSelf);
        }
    }

    public void GiveItem(int id){
        Item itemToAdd = itemDatabase.GetItem(id);
        characterItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd);
        Debug.Log("Added item: " + itemToAdd.title);
    }

    public void GiveItem(string itemName){
        Item itemToAdd = itemDatabase.GetItem(itemName);
        characterItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd);
        Debug.Log("Added item: " + itemToAdd.title);
    }

    public Item CheckForItem(int id){
        return characterItems.Find(item => item.id == id);
    }

    public void RemoveItem(int id){
        Item itemToRemove = CheckForItem(id);
        if (itemToRemove != null){
            characterItems.Remove(itemToRemove);
            inventoryUI.RemoveItem(itemToRemove);
            Debug.Log("Removed item: " + itemToRemove.title);
        }
    }

    public void EquipItem(int id){
        Item itemToEquip = CheckForItem(id);
        if (itemToEquip != null){
            inventoryUI.EquipItem(itemToEquip);
        }
    }

    public void UnequipItem(int id){
        Item itemToUnequip = CheckForItem(id);
        if(itemToUnequip != null){
            equipmentUI.UnequipItem(itemToUnequip);
        }
    }
}
