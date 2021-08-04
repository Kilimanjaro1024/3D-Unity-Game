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
    public LootObject lootObject;

    private void Awake(){
        spriteImage = GetComponent<Image>();
        UpdateItem(null);
        selectedItem = GameObject.Find("SelectedItem").GetComponent<UIItem>();
        tooltip = GameObject.Find("Tooltip").GetComponent<Tooltip>();
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        player = GameObject.Find("Main Camera").GetComponent<Player>();
        dropLocation = GameObject.Find("Drop Location").GetComponent<Transform>();
        selected = false;
        
    }

    private void Update()
    {
        //FIX NULL REFERENCE ERRORS THAT OCCUR WHEN EQUIPPING ITEMS
        HandleInventory();
    }

    private void HandleInventory()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (selected)
            {
                if (this.item.equipable)
                {
                    if (!equipped)
                    {
                        HandleEquip();
                    }
                    else
                    {
                        HandleUnequip();
                    }
                }
                else
                {
                    Debug.Log(this.item.title + " cannot be equiped.");
                }
            }
        }
    }

    //When an item is unequipped this function takes care of setting the equipped state of the slot to false, removing the item script from the equipped list, Destroying the gameObject of the equipped Item
    // and sends the item back to the inventory.
    private void HandleUnequip()
    {
        for (int i = 0; i < player.slots.Count; i++)
        {
            if (this.item.slot == player.slots[i].slotName)
            {
                player.slots[i].filled = false;
                player.slots[i].item = null;
                GameObject item = GameObject.Find(player.slots[i].slotName).transform.GetChild(0).gameObject;
                DestroyImmediate(item, true);
                player.itemSlots[i] = null;
                inventory.UnequipItem(this.item.id);
            }
        }
    }

    //When an Item is equipped this function checks if the item slot is filled and if not spawns the object in gamespace and adds it to the players equipment and removes it from the inventory UI.
    //If the Slot is filled this function swaps the the currently equipped item with the item targeted for equipping.
    private void HandleEquip(){
        for (int i = 0; i < player.slots.Count; i++){
            if(this.item.slot == player.slots[i].slotName){
                if(!player.slots[i].filled){
                    player.slots[i].filled = true;
                    player.slots[i].item = this.item;     
                    player.WearItem();
                    inventory.EquipItem(this.item.id);
                }
                else{
                    GameObject item = GameObject.Find(player.slots[i].slotName).transform.GetChild(0).gameObject;
                    inventory.UnequipItem(player.slots[i].item.id);
                    DestroyImmediate(item, true);
                    player.itemSlots[i] = null;
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
                if(this.gameObject.tag == "Loot"){
                    //  Item clone = new Item(selectedItem.item);
                    inventory.GiveItem(this.item.id);
                    UpdateItem(null);
                    lootObject.loot.RemoveAt(0);
                    lootObject.items.RemoveAt(0);
                    Debug.Log(lootObject.loot.Count);
                    Debug.Log("This is loot");
                }
                else{
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
