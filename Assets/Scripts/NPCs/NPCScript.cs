using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    public NPC npc;

    public bool alive;
    public int health;
    public string nameText;
    
    void OnTriggerEnter(Collider other){
        if(other.tag == "Weapon"){
            Debug.Log(other.gameObject.GetComponent<ItemObject>().swinging);
            Debug.Log("Hit Detected");
            foreach (var stat in other.gameObject.GetComponent<ItemObject>().stats){
                if(stat.Key.ToString() == "Power"){
                    health -= stat.Value;
                }
            }
            CheckAlive();
        }
    }

    void CheckAlive(){
        if (health <= 0){
            alive = false;
            gameObject.GetComponent<Rigidbody>().freezeRotation = false;
        }
    }


}
