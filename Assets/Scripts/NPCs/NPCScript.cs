using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCScript : MonoBehaviour
{
    public NPC npc;
    public bool alive;
    public int health;
    public string nameText;
    

    //CHANGE Damage Calculation to happen in the Mainhand script
    void OnTriggerEnter(Collider other){
        if(other.tag == "Weapon" && other.gameObject.GetComponent<ItemObject>().swinging){
            Debug.Log("Detect Hit");
            health -= other.gameObject.GetComponentInParent<Transform>().gameObject.GetComponentInParent<MainHand>().damage;
            CheckAlive();
        }
    }

    void CheckAlive(){
        if (health <= 0){
            alive = false;
            gameObject.GetComponent<Rigidbody>().freezeRotation = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Rigidbody>().useGravity = true;
            gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Rigidbody>().useGravity = true;
            gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
            gameObject.transform.GetChild(0).GetComponent<Animator>().enabled = false;
            Debug.Log(gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0));
        }
    }


}
