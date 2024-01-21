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
        public Vector3 rpos;
        public Vector3 rrot;
        public Vector3 bpos;
        public Vector3 brot;
    }
}
