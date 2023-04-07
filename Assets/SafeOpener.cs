using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class SafeOpener : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _dialNumTMP;
    private float _currentRotation;
    private float _updatedRotation;
    private int _tempDialedNum=0;
    private int _currentDialedNum=0;
    private int _dialedNum=0;
    private float _timer=0;
    private float _rotationThreshold=8f;
    private float _setDialThreshold = 2f;
    
    void Awake()
    {
    }

    private void Start()
    {
    }

    private float ChangeRotation(float angle)
    {
        //float result = angle - Mathf.CeilToInt(angle / 360f) * 360f;
        if (angle < 0)
        {
            angle += 360f;
        }
        // else if (angle < 180 && angle > 90)
        // {
        //     angle = 180f - angle;
        // }
        return angle;
    }

    // Update is called once per frame
    void Update()
    {
        _currentRotation = ChangeRotation(transform.localEulerAngles.x);
        _dialedNum=RecheckDialNum();
        
        _dialNumTMP.text = ((int)_currentRotation).ToString();
        if (_currentRotation < 180 && _currentRotation > 90)
        {
            Debug.Log(_currentRotation);
        }
        // if (_dialedNum < 10)
        // {
        //     _dialNumTMP.text = _dialedNum.ToString();
        // }
    }

    private int FindDialNum()
    {
        var _remainer = _currentRotation % 36f;
        if (_remainer <= _rotationThreshold)
        {
            return (int)(_currentRotation / 36f);
        }
        else
        {
            Debug.Log("Not turned to the right angle.");
            return 10;
        }
    }
    private int RecheckDialNum()
    {
        _timer += Time.deltaTime;
        if (_timer < 2 * Time.deltaTime)
        {
            _tempDialedNum= FindDialNum();
            //Debug.Log("Temp dial number is "+_tempDialedNum+" and current angle is "+_currentRotation);
        }
        else if (_timer >= _setDialThreshold)
        {
            _currentDialedNum = FindDialNum();
            _timer = 0;
            //Debug.Log("Current dialed number is "+_currentDialedNum+" and current angle is "+_currentRotation);
        }
        if (_currentDialedNum == _tempDialedNum)
        {
            //Debug.Log("You set dial number to "+_currentDialedNum+" and current angle is "+_currentRotation);
            return _currentDialedNum;
        }
        else
        {
            return 10;
        }
    }
}
