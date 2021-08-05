using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootObject : MonoBehaviour, IInteractable
{
    public List<Item> loot = new List<Item>();
    public int[] table = { 
        60,
        30, 
        10
    };

    public int[] lootTable = {

    };
    public int total;
    public int randomNumber;
    public int itemsToDrop;
    public string nameText;
    public GameObject lootUI;
    public ItemDatabase itemDatabase;
    public CharacterController controller;
    public GameObject invetoryUI;
    public GameObject slotPrefab;
    
    void Awake(){
        foreach (var item in table){
            total += item;
        }
        for (int y = 0; y < itemsToDrop; y++){
            randomNumber = Random.Range(0,total);
            for (int x = 0; x < table.Length; x++){
                if(randomNumber <= table[x]){
                    // GameObject instance = Instantiate(slotPrefab);
                    Debug.Log(lootTable[x]);
                    Item itemToAdd = itemDatabase.GetItem(lootTable[x]);
                    Debug.Log(itemToAdd);
                    loot.Add(itemToAdd);
                    break;
                }
                else{
                    randomNumber -= table[x];
                }
            }
        }
    }

    void UIController(){
        controller.m_Pause = !controller.m_Pause;
        lootUI.gameObject.SetActive(!lootUI.gameObject.activeSelf);
        invetoryUI.gameObject.SetActive(!invetoryUI.gameObject.activeSelf);
    }

    public string NameText{
        get{
            return nameText;
        }
    }

    public void DoActivate()
    {
        for (int i = 0; i < lootUI.GetComponent<UILoot>().uIItems.Count; i++)
        {
            lootUI.GetComponent<UILoot>().uIItems[i].UpdateItem(null);
        }
        
        lootUI.GetComponent<UILoot>().lootObject = gameObject.GetComponent<LootObject>();
        lootUI.GetComponent<UILoot>().DetermineSlots(itemsToDrop);
        Debug.Log(loot.Count);
        UIController();
        for (int i = 0; i < loot.Count; i++){
            Debug.Log(loot[i]);
            lootUI.GetComponent<UILoot>().AddNewItem(loot[i]);
            Debug.Log(lootUI.GetComponent<UILoot>().uIItems[i]);
            lootUI.GetComponent<UILoot>().uIItems[i].lootObject = gameObject.GetComponent<LootObject>();
        }
    }
}
