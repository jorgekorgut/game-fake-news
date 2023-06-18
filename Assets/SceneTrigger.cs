using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Platformer.Mechanics;

public class SceneTrigger : MonoBehaviour
{
    public SceneHandler dialog;
    public InteractableHandler interactable;
    public String id;

    private bool isInsideTrigger = false;
    private bool isActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }   

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && isInsideTrigger)
        {
            interactable.displayInteractable(false);
            dialog.displayDialog(id);
            isActivated = true;
        }

        if(isActivated && !isInsideTrigger)
        {
            dialog.hideDialog();
            isActivated = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        interactable.displayInteractable(true);
        isInsideTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {   
        interactable.displayInteractable(false);
        isInsideTrigger = false;
    }
}
