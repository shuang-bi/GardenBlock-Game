using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;


public class SafeOpener : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _dialNumTMP;
    [SerializeField] private TextMeshProUGUI _givenNumTMP;
    [SerializeField] private ParticleSystem _successFire;
    private float _currentRotation;
    private float _updatedRotation;
    private int _dialedNum=10;
    private float _rotationThreshold=0.3f;
    private float _setDialThreshold = 2f;
    private Vector3 _initialDirection;
    private int[] _enteredNums = new int[]{10,10,10};
    private int[] _givenNums= new int[3];
    private int _i = 0;
    private bool _newDialedNum = false;
    void Awake()
    
    {
    }
    private void Start()
    {
        _successFire.Stop();
        _initialDirection = transform.right;
        _givenNums[0] = (int)Random.Range(0f, 9f);
        _givenNums[1] = (int)Random.Range(0f, 9f);
        _givenNums[2] = (int)Random.Range(0f, 9f);
        _givenNumTMP.text= "Code "+_givenNums[0].ToString() + ", " +_givenNums[1]+ ", " +_givenNums[2];
    }

    private float ChangeRotation(float angle)
    {
        if (angle < 0) { angle = Mathf.Abs(angle);; }
        else if(angle > 0) { angle = Mathf.Abs(angle - 180) + 180f; }
        return angle;
    }

    // Update is called once per frame
    void Update()
    {
        _currentRotation = ChangeRotation(Vector3.SignedAngle(_initialDirection, transform.right, transform.up));
        UpdateDialNum();
        var _enteredNumText = "Entering "+_enteredNums[0].ToString() + ", " +_enteredNums[1]+ ", " +_enteredNums[2];
        _dialNumTMP.text = _enteredNumText;
        CheckSuccess();
    }

    private void CheckSuccess()
    {
        if (_enteredNums[0] == _givenNums[0] && _enteredNums[1] == _givenNums[1] && _enteredNums[2] == _givenNums[2])
        {
            _successFire.Play();
            Debug.Log("Success!");
        }
    }

    private void UpdateDialNum()
    {
        if (_newDialedNum&&_dialedNum<10)
        {
            _enteredNums[_i % 3] = _dialedNum;
            _i++;
            _newDialedNum = false;
        }
    }


    public void FindDialNum()
    {
        var _decimal = (float)(_currentRotation / 36f)-(int)(_currentRotation / 36f);
        if (_decimal <= _rotationThreshold)
        {
            _dialedNum= (int)(_currentRotation / 36f);
            _newDialedNum = true;
            Debug.Log("New num is "+_newDialedNum);
        }
        else if(_decimal>=1-_rotationThreshold)
        {
            _dialedNum= (int)(_currentRotation / 36f +1f);
            _newDialedNum = true;
            Debug.Log("New num is "+_newDialedNum);
        }
        else
        {
            Debug.Log("Not turned to the right angle.");
        }
    }
}
