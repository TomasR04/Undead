using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleWindow : MonoBehaviour
{
    public GameObject sisterWindow;
    public bool isOpen = false;
    // Start is called before the first frame update


    public void Open()
    {
        gameObject.transform.Rotate(Vector3.down, 180f);
        sisterWindow.transform.Rotate(Vector3.up, 180f);
        isOpen = !isOpen;
    }

    public void Close()
    {
        gameObject.transform.Rotate(Vector3.up, 180f);
        sisterWindow.transform.Rotate(Vector3.down, 180f);
        isOpen = !isOpen;
    }
}
