using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayersInteractionsWithObjects : MonoBehaviour
{
    public TMP_Text text;
    public LayerMask layerMask;
    public Camera camera;
    public GameObject inventoryItems;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        //Physics.Raycast(gameObject.transform.position, transform.forward, out hit, 5f, 6);   
        //var ray = Physics.Raycast(transform.position, Vector3.forward,out hit, 5f, 6);
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 2f, layerMask))
        {

            //Debug.Log(hit.collider.name, hit.collider);
            GameObject a = hit.collider.gameObject;
            if (a.tag == "Door")
            {
                Door door = a.GetComponent<Door>();
                if (!(door.needsKey))
                {
                    if (door.isOpen)
                    {
                        text.text = "F zavøít";
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            door.Close();
                        }
                    }
                    else
                    {
                        text.text = "F otevøít";
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            door.Open();
                        }
                    }
                }
                else
                {
                    GameObject neededKey = door.key;
                    Transform myKey = inventoryItems.transform.Find(neededKey.name);

                    Debug.Log(neededKey);
                    if (myKey != null)
                    {
                        if (door.isOpen)
                        {
                            text.text = "F zavøít";
                            if (Input.GetKeyDown(KeyCode.F))
                            {
                                door.Close();
                            }
                        }
                        else
                        {
                            text.text = "F otevøít";
                            if (Input.GetKeyDown(KeyCode.F))
                            {
                                door.Open();
                            }
                        }
                    }
                    else
                    {
                        text.text = "Nemáš klíè";
                    }
                }

            }
            else if (a.tag == "Window")
            {
                DoubleWindow doubleWindow = a.GetComponent<DoubleWindow>();
                if (doubleWindow.isOpen)
                {
                    text.text = "F zavøít";
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        doubleWindow.Close();
                    }
                }
                else
                {
                    text.text = "F otevøít";
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        doubleWindow.Open();
                    }
                }
            }

        }
        else
        {
            text.text = "";
        }

    }
}
