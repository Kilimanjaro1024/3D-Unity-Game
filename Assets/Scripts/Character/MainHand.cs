using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHand : MonoBehaviour
{
    public Animator Anim;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        QuickAttack();
    }

    public void QuickAttack(){
        if(Input.GetButtonDown("Fire1")){
            Anim.SetBool("swing", true);   
        }
        else{
            Anim.SetBool("swing", false);
        }
    }
}
