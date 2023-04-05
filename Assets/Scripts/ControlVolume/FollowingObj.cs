using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class FollowingObj : MonoBehaviour
{
    [SerializeField] ControlVolume _controlVolume;
    
    private Vector3 _moveDiff;
    private float _rotateDiff;
    
    void Start()
    {
        _moveDiff = transform.position - _controlVolume.transform.position;

    }
    
    void Update()
    {
        if (_controlVolume.moving)
        {
            MoveAlong();
        }
        if (_controlVolume.rotate)
        {
            Rotate();
        }
    }
    private void MoveAlong()
    {
        transform.position = _controlVolume.newTransform.position + _moveDiff;

    }
    private void Rotate()
    {
        transform.rotation = Quaternion.Euler(_controlVolume.eulerZ,0,0);
    }
}
