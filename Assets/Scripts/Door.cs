using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{
    //public GameObject key;
    public bool isOpen = false;
    public bool isLocked = false;
    public bool needsKey;
    public GameObject key;
    public NavMeshLinkData navlink;

    void Start()
    {
        if (key != null)
        {
            needsKey = true;
        }
        else
        {
            needsKey = false;
        }
    }


    public void Open()
    {
        gameObject.transform.Rotate(Vector3.down, 90f);
        isOpen = !isOpen;

    }
    public void Close()
    {
        gameObject.transform.Rotate(Vector3.up, 90f);
        isOpen = !isOpen;
    }
    public void Lock()
    {
        isLocked = true;
    }

    public void UnLock()
    {
        isLocked = false;
    }
}
