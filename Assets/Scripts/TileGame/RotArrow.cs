using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RotArrow : XRBaseInteractable
{
    [SerializeField] private GameObject _socketCube;
    [SerializeField] private HideArrow _hideArrow;
    [SerializeField] private Material _activeMat;

    private Material _oldMat;
    private Vector3 _offsetVector3;


    public void RotateEnter()
    {
        GetComponent<Renderer>().material = _activeMat;
        transform.position += _offsetVector3;
    }
    public void RotateExit()
    {
        GetComponent<Renderer>().material = _oldMat;
        transform.position -= _offsetVector3;
        _socketCube.transform.Rotate(0, 90f, 0, Space.Self);
    }

    void Start()
    {
        _oldMat = GetComponent<Renderer>().material;
        _offsetVector3= new Vector3(0.02f, 0, 0);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        RotateEnter();
    }
    private void OnTriggerExit(Collider other)
    {
        RotateExit();
    }
    

    // Update is called once per frame
    void Update()
    {
        if (_hideArrow.showArrow)
        {
            GetComponent<Renderer>().enabled = true;
        }
        else if (!_hideArrow.showArrow)
        {
            GetComponent<Renderer>().enabled = false;
        }

    }
}
