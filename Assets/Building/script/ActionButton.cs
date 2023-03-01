using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{


    public GameObject[] objects;
    private GameObject pendingObj;

    private Vector3 pos;

    private RaycastHit hit;
    [SerializeField] private LayerMask layerMask;

    void Update()
    {
        if (pendingObj != null)
        {
            
            pendingObj.transform.position = pos;
            if (Input.GetMouseButtonDown(0))
            {

                PlaceObject();
            }
            
        }
    }

    void PlaceObject()
    {
        pendingObj = null;
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            pos = hit.point;
        }


    }

    public void SelectObject(int index)
    {
        pendingObj = Instantiate(objects[index], pos, transform.rotation);
    }


    

}
