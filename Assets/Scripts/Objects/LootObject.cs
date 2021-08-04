using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootObject : MonoBehaviour, IInteractable
{
    public List<Item> loot = new List<Item>();
    public List<UIItem> items = new List<UIItem>();
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
    public bool Looted;

    public GameObject slotPrefab;
    // void Update(){
    //     Debug.Log(loot.Count);
    // }
    void Awake(){
        // invetoryUI = GameObject.Find("InevtoryPanel").GetComponent<GameObject>();
        for (int y = 0; y < itemsToDrop; y++){
            foreach (var item in table){
                total += item;
            }
            randomNumber = Random.Range(0,total);
                for (int x = 0; x < table.Length; x++){
                    if(randomNumber <= table[x]){
                        GameObject instance = Instantiate(slotPrefab);
                        Debug.Log(lootTable[x]);
                        Item itemToAdd = itemDatabase.GetItem(lootTable[x]);
                        Debug.Log(itemToAdd);
                        loot.Add(itemToAdd);
                        items.Add(instance.GetComponentInChildren<UIItem>());
                        // lootUI.GetComponent<UILoot>().AddNewItem(itemToAdd);
                        
                        // return;
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
        lootUI.GetComponent<UILoot>().lootObject = gameObject.GetComponent<LootObject>();
        lootUI.GetComponent<UILoot>().DetermineSlots(itemsToDrop);
        UIController();
        for (int i = 0; i < loot.Count; i++){
            lootUI.GetComponent<UILoot>().AddNewItem(loot[i]);
            Debug.Log(lootUI.GetComponent<UILoot>().uIItems[i]);
            lootUI.GetComponent<UILoot>().uIItems[i].lootObject = gameObject.GetComponent<LootObject>();
        }
    }
}
