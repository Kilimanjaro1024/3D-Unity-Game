using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHand : MonoBehaviour
{
    public Animator Anim;
    public float timer;
    public bool strong;
    public ItemObject heldItem;
    
    // void Awake(){
    //     UpdateHeldItem();
    // }
    // Update is called once per frame
    void Update()
    {
        if(heldItem != null){
            SetSwinging();
            HandleAttack();
        }
    }

    private void SetSwinging()
    {
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("SwingWeapon-Quick") || Anim.GetCurrentAnimatorStateInfo(0).IsName("SwingWeapon-StrongAttack")){
            heldItem.swinging = true;
            Debug.Log(heldItem.swinging);
        }
        else{
            heldItem.swinging = false;
            Debug.Log("False");
        }
    }

    private void HandleAttack()
    {
        if (Input.GetButton("Fire1")){
            timer += Time.deltaTime;
            if(timer >= 1){
                strong = true;
                Anim.SetBool("strong", true);
            }
        }
        if (Input.GetButtonUp("Fire1")){
            if (strong) {
                StrongAttack();
                Debug.Log("STRONG");
            }
            else {
                QuickAttack();
            }
            // Anim.SetBool("swing", false);
            timer = 0;
        }
        else{
            Anim.SetBool("swing", false);
           
        }
    }

    public void QuickAttack(){
        Debug.Log("QUICK");
        Anim.SetBool("swing", true);  
    }

    public void StrongAttack(){
        Anim.SetBool("swing", true); 
        Anim.SetBool("strong", false); 
    }

    public void UpdateHeldItem(){
        if(gameObject.transform.GetChild(0) != null){
            heldItem = gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<ItemObject>();
        }
    }
}
