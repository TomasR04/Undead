using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsList : MonoBehaviour
{
    public List<ItemSprite> items = new List<ItemSprite>();
    [Serializable]
    public class ItemSprite
    {
        public GameObject item;
        public Sprite sprite;
    }
}
