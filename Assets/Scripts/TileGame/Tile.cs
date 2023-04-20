using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Tile : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable _grabInteractable;
    //[SerializeField] private Transform _gunBarrel;
    [SerializeField] private XRSocketInteractor _socket1;
    // Start is called before the first frame update
    void Start()
    {
        _socket1.selectEntered.AddListener(Attached);
        _socket1.selectExited.AddListener(Detached);
    }
    private void Attached(SelectEnterEventArgs arg0)
    {
        IgnoreCollision(arg0.interactable,true);
    }

    private void Detached(SelectExitEventArgs arg0)
    {
        IgnoreCollision(arg0.interactable,false);
    }
    private void IgnoreCollision(XRBaseInteractable arg0Interactable, bool b)
    {
        var myColliders = GetComponentsInChildren<Collider>();
        foreach (var myCollider in myColliders)
        {
            foreach (var interactableCollider in _grabInteractable.colliders)
            {
                Physics.IgnoreCollision(myCollider,interactableCollider);
            }
        }
        Debug.Log("Ignoring collision "+ b);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
