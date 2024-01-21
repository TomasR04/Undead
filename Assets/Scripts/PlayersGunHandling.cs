using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayersGunHandling : MonoBehaviour
{
    public Animator animator;
    public Transform firstPersCam;
    public GameObject GunInHand;
    public Transform RHand;
    public GameObject cineBrain;
    public GameObject globalServicies;
    public Transform head;
    public GameObject inventory;
    public GameObject smallSlots;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        //RHand = gameObject.GetComponent<PlayersInventory>().RHand;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("HasRifle"))
        {
            GunInHand = WhatIsInRightHand();
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                animator.SetBool("IsAiming", true);
                GunInHand.transform.localPosition = new Vector3(0.077f, 0.057f, -0.022f);
                GunInHand.transform.localEulerAngles = new Vector3(190.089f, -114.346f, -271.387f);
                firstPersCam.transform.parent = GunInHand.transform;
                firstPersCam.transform.localPosition = GunInHand.transform.Find("CamPos").localPosition;
                firstPersCam.transform.localEulerAngles = new Vector3(0, 0, 0);
                firstPersCam.GetComponent<CinemachineVirtualCamera>().m_Lens.NearClipPlane = 0.01f;

                

            }
            else if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                animator.SetBool("IsAiming", false);
                firstPersCam.transform.parent = transform;
                firstPersCam.transform.localPosition = new Vector3(0f, 1.658f, 0.255f);
                firstPersCam.GetComponent<CinemachineVirtualCamera>().m_Lens.NearClipPlane = 0.3f;
                GunInHand.transform.localPosition = globalServicies.GetComponent<ItemsList>().items[1].rpos;
                GunInHand.transform.localEulerAngles = globalServicies.GetComponent<ItemsList>().items[1].rrot;

            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GunInHand.GetComponent<Gun>().Fire();
                //Debug.Log("Kliknuto");
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                //firstPersCam.parent = head;
                firstPersCam.GetComponent<CinemachineVirtualCamera>().m_Lens.NearClipPlane = 0.3f;
                UIInventorySlot[] sloty = smallSlots.GetComponentsInChildren<UIInventorySlot>();
                List<Magazine> mags = new List<Magazine>();
                foreach (var item in sloty)
                {
                    if (item.objekt.GetComponent<Magazine>())
                    {
                        mags.Add(item.objekt.GetComponent<Magazine>());
                    }
                }
                foreach (var item in mags)
                {
                    if (item.ammo>0) 
                    {
                        if (GunInHand.GetComponentInChildren<Magazine>())
                        {
                            Vector3 pos = GunInHand.GetComponent<Gun>().magazine.transform.localPosition;
                            Vector3 rot = GunInHand.GetComponent<Gun>().magazine.transform.localEulerAngles;
                            GetComponent<PlayersInventory>().PickSmall(GunInHand.GetComponent<Gun>().magazine.gameObject);
                        }

                        GameObject objekt = item.gameObject;
                        objekt.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                        objekt.transform.parent = GunInHand.transform;
                        objekt.transform.localPosition = new Vector3(0, 0.03115405f, 0.1488003f);
                        objekt.transform.localEulerAngles = new Vector3(0, 0, 0);
                        objekt.GetComponent<MeshRenderer>().enabled = true;
                        
                        objekt.GetComponent<Collider>().enabled = true;
                        GetComponent<PlayersInventory>().usedCapacity -= objekt.GetComponent<Item>().size;
                        GetComponent<PlayersInventory>().usedWeight -= objekt.GetComponent<Item>().weight;
                        UIInventorySlot[] slots = smallSlots.GetComponentsInChildren<UIInventorySlot>();
                        GameObject toDestroy = item.gameObject;
                        foreach (UIInventorySlot item1 in slots)
                        {
                            if (item1.objekt == item.gameObject)
                            {
                                toDestroy = item1.gameObject;
                                break;
                            }
                        }
                        Destroy(toDestroy);
                        animator.Play("Stand Reload");
                        break;


                    }
                }
                
            }

        }
    }
    private GameObject WhatIsInRightHand()
    {
        Transform[] childs = RHand.transform.GetComponentsInChildren<Transform>();
        List<Transform> childsInList = new List<Transform>();
        GameObject result = null;
        for (int i = 0; i < childs.Length; i++)
        {
            if (childs[i].tag=="Rifle")
            {
                result = childs[i].gameObject;
                break;
            }
        }
        return result;
    }
}
