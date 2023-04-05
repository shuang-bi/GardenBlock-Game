using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TreeEditor;
using UnityEditor.XR.Interaction.Toolkit.AR;
using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = System.Numerics.Vector3;

public class ControlVolume : MonoBehaviour
{
    public bool startInteraction=false;
    public bool moving=false;
    public bool rotate = false;
    
    public Transform newTransform;
    public float eulerZ;
    
    private float _movingThreshold=0.2f;
    private Rigidbody _handRb;

    private float _timer = 0;
    private float _rotationThreshold=1f;
    private float _oldEulerZ;
    private float _newEulerZ;
    private Transform _oldTransform;
    private float _rotationAmount;
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Hand"))
        {
            startInteraction = true;
            _handRb = collider.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Hand"))
        {
            startInteraction = false;
            moving = false;
            rotate = false;
            Debug.Log("Exited interaction");
        }
    }
    void Start()
    {
        _oldTransform = transform;
        newTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (startInteraction)
        {
            StoreTransform();
            if (_handRb.velocity.normalized.z>_movingThreshold)
            {
                moving = true;
            }
            if (_rotationAmount> _rotationThreshold)
            {
                rotate = true;
                eulerZ = _handRb.transform.rotation.eulerAngles.z;
            }
        }
    }

    private void StoreTransform()
    {
        _timer += Time.deltaTime;
        if (_timer < 2*Time.deltaTime&& _timer >0)
        {
            _oldTransform = _handRb.transform;
            _oldEulerZ = _oldTransform.rotation.eulerAngles.z;
        }
        if (_timer >= 0.5f)
        {
            newTransform = _handRb.transform;
            _newEulerZ = newTransform.rotation.eulerAngles.z;
            _rotationAmount = _oldEulerZ- _newEulerZ;
            _timer = 0;
        }
    }


}
