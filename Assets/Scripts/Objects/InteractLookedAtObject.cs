using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InteractLookedAtObject : MonoBehaviour
{
    private float maxdistanceToActivate = 4;
    public LayerMask layerToCheckForObjects;
    public Text lookedAtObjectText;

    IInteractable lookedAtObject;

    private void Update(){
        CheckForLookedAtObjects();
        UpdateLookedAtObjects();
        HandleInput();
    }

    private void HandleInput(){
        if(Input.GetButtonDown("Fire1")){
            Debug.Log("Button Clicked");
            if(lookedAtObject != null){
                lookedAtObject.DoActivate();
            }
        }
    }

    private void UpdateLookedAtObjects(){
        if(lookedAtObject != null){
            lookedAtObjectText.text = lookedAtObject.NameText;
            lookedAtObjectText.gameObject.SetActive(true);
        }
        else{
            lookedAtObjectText.gameObject.SetActive(false);
        }
    }

    private void CheckForLookedAtObjects(){
        Vector3 endPoint = (transform.forward * maxdistanceToActivate) + transform.position;
        Debug.DrawLine(transform.position, endPoint, Color.red);

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, maxdistanceToActivate, layerToCheckForObjects)){
            lookedAtObject = hit.transform.gameObject.GetComponent<IInteractable>();
        }
        else{
            lookedAtObject = null;
        }
    }
}
