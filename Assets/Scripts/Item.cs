using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public double hp = 1;
    public double weight = 1;
    public double size = 1;
    public double food = 0;
    public double drink = 0;


    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
