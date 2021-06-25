using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public List<Slot> slots = new List<Slot>();

    public GameObject[] itemSlots;
    public Transform mainHand;
    
    void Awake(){
        BuildSlots();

    }

    private void BuildSlots(){
        slots = new List<Slot>(){
            new Slot(0, "Main Hand", false, null),
            new Slot(1, "Head", false, null),
            new Slot(2, "Chest", false, null)
        };
        itemSlots = new GameObject[slots.Count];
        WearItem();
    }

    //Spawns the gameobject of an equipped item in the proper slot on the player.
    public void WearItem(){
        for (int i = 0; i < slots.Count; i++){
            if(slots[i].filled){
                itemSlots[i] = Resources.Load<GameObject>("Items/" + slots[i].item.title);
                GameObject item = Instantiate(itemSlots[i], GameObject.Find(slots[i].slotName).transform.position,  new Quaternion(0, 0, 0, 0));
                item.transform.parent = GameObject.Find(slots[i].slotName).transform;
                item.GetComponent<Rigidbody>().isKinematic = true;
                // item.GetComponent<Rigidbody>().freezeRotation = true;
                item.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
                // item.transform.GetChild(0).GetComponent<Rigidbody>().freezeRotation = true;
                item.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    public void Attack(){
        if(Input.GetButtonDown("Fire1")){

        }
    }
}
