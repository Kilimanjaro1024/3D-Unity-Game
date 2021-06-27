using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    public NPC npc;

    public bool alive;
    public int health;
    public string nameText;

    void Awake(){
        // alive = npc.alive;
        // health = npc.stats["Health"];
        // nameText = npc.name;
    }
    
    void OnTriggerEnter(Collider other){
        if(other.tag == "Weapon"){
            Debug.Log(other.gameObject.GetComponent<ItemObject>().swinging);
            Debug.Log("Hit Detected");
        }
    }


}
