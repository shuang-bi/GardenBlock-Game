using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class HideArrow : XRBaseInteractable
{

    public bool showArrow=false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==10){showArrow = true;}
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer==10){showArrow = false;}
    }
}
