using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Tile1 : MonoBehaviour
{
    public bool _inSocket=false;
    [SerializeField] private GameManager _gameManager;

    private XRGrabInteractable _grabInteractable;

    private Transform _startTransform;
    private bool _assignedDup = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _grabInteractable = GetComponent<XRGrabInteractable>();
        
    }

    private void Awake()
    {
        _startTransform = gameObject.transform;
    }


    // Update is called once per frame
    void Update()
    {
        CheckInSocket();
        AssignDupTile();
    }

    private void AssignDupTile()
    {
        if (_grabInteractable.isSelected &&!_assignedDup)
        {
            _gameManager.dupTileTransform = gameObject.transform;
            _gameManager.dupTileObj = gameObject;
            _assignedDup = true;
        }
    }

    public void CheckInSocket()
    {
        if (!_inSocket&&_grabInteractable.isSelected)
        {
            var interactors = _grabInteractable.interactorsSelecting;
            foreach (var interactor in interactors)
            {
                if (interactor is XRSocketInteractor)
                {
                    _inSocket=true;
                    _gameManager.allTiles.Add(gameObject);
                }
            }
        }
    }

}
