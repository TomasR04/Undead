using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public double hp = 5;
    public int numberOfLogs = 1;
    public GameObject log;
    //public GameObject stump;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hp < 1)
        {
            Vector3 victor = transform.position;
            victor.y -= 0.2f;
            //Instantiate(stump, victor, transform.rotation);
            for (int i = 0; i < numberOfLogs; i++)
            {
                Vector3 vector = transform.position;
                vector.y += i * 1.2f;
                Instantiate(log, vector, transform.rotation);
            }


            Destroy(gameObject);
        }
    }
    public void RecivedHit(double damage)
    {
        hp -= damage;
        Debug.Log(hp);
    }
}
