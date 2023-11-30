using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour
{
    public Action<GameObject> clicked;
    public GameObject objekt;
    private Sprite _sprite;
    private Button button;
    public Sprite Sprite {
        set { 
            gameObject.GetComponent<Image>().sprite = value;
            _sprite = value;
        }
        get => _sprite;
    }
    private void Awake()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(Click);
    }

    private void Click()
    {
 
            clicked.Invoke(gameObject);
            Debug.Log("Kliknuto");

        
    }
}
