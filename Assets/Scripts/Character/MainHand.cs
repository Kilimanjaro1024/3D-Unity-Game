using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHand : MonoBehaviour
{
    public Animator Anim;
    public float timer;
    public bool strong;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        HandleAttack();
    }

    private void HandleAttack()
    {
        if (Input.GetButton("Fire1"))
        {
            timer += Time.deltaTime;
            if(timer >= 1){
                strong = true;
                Anim.SetBool("strong", true);
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
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
}
