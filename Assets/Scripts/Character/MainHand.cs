using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHand : MonoBehaviour
{
    public Animator Anim;
    public float quickAnim;
    public float strongAnim;
    public float timer;
    public bool strong;
    public ItemObject heldItem;
    public int damage;
    
    void Update()
    {
        if(heldItem != null){
            SetSwinging();
            HandleAttack();
        }
    }

    private void SetSwinging()
    {
        // if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("SwingWeapon-Quick") || !Anim.GetCurrentAnimatorStateInfo(0).IsName("SwingWeapon-StrongAttack")){
        //     heldItem.swinging = false;
        //     Debug.Log("False");
        // }
        // // else{
        // //     heldItem.swinging = false;
        // //     Debug.Log("False");
        // // }
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
                //find a way to specify the stat you are looking for
                
            }
            else {
                QuickAttack();
                //find a way to specify the stat you are looking for
                
            }
            timer = 0;
        }
        else{
            Anim.SetBool("swing", false);
           
        }
    }

    public void QuickAttack(){
        Anim.SetBool("swing", true); 
        // heldItem.swinging = true;
        foreach (var stat in gameObject.GetComponentInChildren<Transform>().gameObject.GetComponentInChildren<ItemObject>().stats){
            if(stat.Key.ToString() == "Power"){
                damage = stat.Value;
            }
        } 
        StartCoroutine(AnimationFinish(quickAnim));
    }

    public void StrongAttack(){
        Anim.SetBool("swing", true); 
        // heldItem.swinging = true;
        foreach (var stat in gameObject.GetComponentInChildren<Transform>().gameObject.GetComponentInChildren<ItemObject>().stats){
            if(stat.Key.ToString() == "Power"){
                damage = (int)Mathf.Ceil(stat.Value * 1.5f);
            }
        }
        Anim.SetBool("strong", false);
        StartCoroutine(AnimationFinish(strongAnim)); 
        
    }

    public void UpdateHeldItem(){
        if(gameObject.transform.GetChild(0) != null){
            heldItem = gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<ItemObject>();
        }
    }
    
    IEnumerator AnimationFinish(float attckDuration){
        heldItem.swinging = true;
        yield return new WaitForSeconds(attckDuration);
        heldItem.swinging = false;

    }
}
