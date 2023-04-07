using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Rotator : MonoBehaviour
{
    private int _rotationAmountUnit=36;
    //What allows user to interact
    private XRBaseInteractor _interactor;
    private float _startAngle=0f;
    private bool _startHandRotation=false;
    
    //GameObjects that user can interact with
    private XRGrabInteractable _grabInteractable => GetComponent<XRGrabInteractable>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        _grabInteractable.selectEntered.AddListener(GrabbedBy);
        _grabInteractable.selectExited.AddListener(GrabEnd);
    }
    private void OnDisable()
    {
        _grabInteractable.selectEntered.RemoveListener(GrabbedBy);
        _grabInteractable.selectExited.RemoveListener(GrabEnd);
    }

    private void GrabbedBy(SelectEnterEventArgs arg0)
    {
       //Get the "hand" that interacts with this GrabInteractable
        _interactor = GetComponent<XRGrabInteractable>().selectingInteractor;
        _startHandRotation = true;
    }

    private void GrabEnd(SelectExitEventArgs arg0)
    {
        _startHandRotation = false;
    }
    void Update()
    {
        if (_startHandRotation)
        {
            var _rotationAngle = _interactor.GetComponent<Transform>().eulerAngles.z;
            GetRotationDegree(_rotationAngle);
        }
    }

    private void GetRotationDegree(float currentAngle)
    {
        var _angleDiff = Mathf.Abs(_startAngle - currentAngle);
        if (_angleDiff > 270f)
        {
            var _angleCheck= CheckAngle(currentAngle, _startAngle);
            if (_startAngle < currentAngle)
            {
                RotateClockwise();
                
            }
            else
            {
                RotateAntiClockwise();
            }
        }
        _startAngle = currentAngle;
        
    }
    private void RotateClockwise()
    {
        transform.localEulerAngles = new Vector3();
    }

    private void RotateAntiClockwise()
    {
        throw new NotImplementedException();
    }

    

    private float CheckAngle(float currentAngle, float startAngle) => (360f - currentAngle) + startAngle;
}
