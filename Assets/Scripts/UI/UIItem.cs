using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    private Image spriteImage;
    private UIItem selectedItem;
    private Tooltip tooltip;
    public Inventory inventory;
    public GameObject prefab;
    private Transform dropLocation;
    
    private bool selected;
    public bool equipped;
    public Player player;

    private void Awake(){
        spriteImage = GetComponent<Image>();
        UpdateItem(null);
        selectedItem = GameObject.Find("SelectedItem").GetComponent<UIItem>();
        tooltip = GameObject.Find("Tooltip").GetComponent<Tooltip>();
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        player = GameObject.Find("Player").GetComponent<Player>();
        dropLocation = GameObject.Find("Drop Location").GetComponent<Transform>();
        selected = false;
    }

    private void Update(){
        //FIX NULL REFERENCE ERRORS THAT OCCUR WHEN EQUIPPING ITEMS
        if(Input.GetKeyDown(KeyCode.E)){
            if(selected){
                if(this.item.equipable){
                    if(!equipped){
                        CheckSlot();   
                    }
                    else{
                        for (int i = 0; i < player.slots.Count; i++){
                            if(this.item.slot == player.slots[i].slotName){
                                player.slots[i].filled = false;
                                player.slots[i].item = null;
                                inventory.UnequipItem(this.item.id);
                            }
                        } 
                    }
                }
                else{
                    Debug.Log(this.item.title + " cannot be equiped.");
                }
            }
        }        
    }

    private void CheckSlot(){
        for (int i = 0; i < player.slots.Count; i++){
            if(this.item.slot == player.slots[i].slotName){
                if(!player.slots[i].filled){
                    player.slots[i].filled = true;
                    player.slots[i].item = this.item;     
                    player.WearItem();
                    inventory.EquipItem(this.item.id);
                }
                else{
                    inventory.UnequipItem(player.slots[i].item.id);
                    player.slots[i].item = this.item;
                    inventory.EquipItem(this.item.id);
                    player.WearItem();
                    Debug.Log("The " + player.slots[i].slotName + " is filled.");
                }
            }
        }
    }

    public void UpdateItem(Item item){
        this.item = item;
        if(this.item != null){
            spriteImage.color = Color.white;
            spriteImage.sprite = this.item.icon;
            prefab = Resources.Load<GameObject>("Items/" + item.title);
        }
        else{
            spriteImage.color = Color.clear;
        }
    }

    public void OnPointerClick(PointerEventData eventData){
        //Pick up and move an item in the inventory
        if(eventData.button == PointerEventData.InputButton.Left){
            if(this.item != null){
                if(selectedItem.item != null){
                    Item clone = new Item(selectedItem.item);
                    selectedItem.UpdateItem(this.item);
                    UpdateItem(clone);
                }
                else{
                    selectedItem.UpdateItem(this.item);
                    UpdateItem(null);
                }
            }
            else if(selectedItem.item != null){
                UpdateItem(selectedItem.item);
                selectedItem.UpdateItem(null);
            }    
        }

        //Remove an Item From the Iventory Menu
        if(eventData.button == PointerEventData.InputButton.Right){
             if(this.item != null){
                Debug.Log("Remove Item");
                inventory.RemoveItem(item.id);
                Instantiate(prefab, dropLocation.position , new Quaternion(0, 0, 0, 0));
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData){
        if(this.item != null){
            tooltip.GenerateTooltip(this.item);
            selected = true;
        }
        
    }
    public void OnPointerExit(PointerEventData eventData){
        tooltip.gameObject.SetActive(false);
        selected = false;
    }
   
}
