using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public List<Slot> slots = new List<Slot>();
    
    void Awake(){
        BuildSlots();
    }

    private void BuildSlots(){
        slots = new List<Slot>(){
            new Slot(0, "Main Hand", false, null),
            new Slot(1, "Head", false, null),
            new Slot(2, "Chest", false, null)
        };
    }
    
}
