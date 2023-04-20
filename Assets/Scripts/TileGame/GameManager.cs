using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class GameManager : MonoBehaviour
{
    public List<GameObject> allTiles = new List<GameObject>();
    public Transform dupTileTransform;
    public GameObject dupTileObj;
    [SerializeField] private Transform _gardenLoc;
    [SerializeField] private Transform _tileOrigin;
    [SerializeField] private Material _pathMat;
    [SerializeField] private GameObject _testPrefab;

    private GameObject _newGarden;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DupTile()
    {
        Debug.Log("duplicating at "+dupTileTransform.position);
        var newTile = Instantiate(dupTileObj, dupTileTransform.position, dupTileTransform.rotation);
        var rb = newTile.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
    }
    public void GenerateGarden()
    {
        _newGarden = new GameObject();
        _newGarden.transform.position = _tileOrigin.transform.position;
        _newGarden.transform.rotation = _tileOrigin.transform.rotation;
        foreach (var gardenTile in allTiles)
        {
            var newTile = Instantiate(gardenTile, gardenTile.transform.position, gardenTile.transform.rotation);
            var _rb=newTile.GetComponent<Rigidbody>();
            _rb.useGravity = false;
            //_rb.isKinematic = true;
            //newTile.GetComponent<TeleportationArea>().enabled=true;

            newTile.GetComponent<XRGrabInteractable>().enabled = false;
            newTile.transform.parent = _newGarden.transform;
        }
        var _rbList = _newGarden.GetComponentsInChildren<Rigidbody>();
        foreach (var rb in _rbList)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
        _newGarden.transform.Rotate(90f,0,0,Space.Self);
        _newGarden.transform.Rotate(0,90f,0,Space.Self);
        _newGarden.transform.position = _gardenLoc.position;

        _newGarden.transform.localScale = new Vector3(20, 20, 20);
        _newGarden.transform.position -= new Vector3(0f, 0.67f, 0f);
        foreach (Transform tile in _newGarden.transform)
        {
            foreach (Transform child in tile.transform)
            {
                if (child.gameObject.GetComponent<Path>())
                {
                    child.position -= new Vector3(0,0.1f,0);
                    //child.localScale = new Vector3(1f, 0.25f, 1f);
                    child.gameObject.GetComponent<Collider>().enabled = false;
                    //child.gameObject.GetComponent<Renderer>().material = _pathMat;
                }
            }
        }

    }
}
