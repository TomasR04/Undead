using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;

public class PlayersToolUsage : MonoBehaviour
{
    public GameObject globalServicies;
    private ItemsList globalItemsList;

    private Animator animator;
    PlayerStats playersStats;
    public Transform eyes;
    public GameObject head;
    public LayerMask layerMask;
    public PlayersInventory inventory;

    public GameObject target;
    private float targetDistanceAtStartOfAnimation;
    public Transform RHand;
    public PlayersInventory playersInventory;
    // Start is called before the first frame update
    void Start()
    {
        globalItemsList = globalServicies.GetComponent<ItemsList>();
        animator = GetComponent<Animator>();
        playersStats = gameObject.GetComponent<PlayerStats>();
        playersInventory = gameObject.GetComponent<PlayersInventory>();
        //RHand = GetComponent<PlayersGunHandling>().RHand;

    }

    private void Update()
    {
        if (!playersInventory.isOpen)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (WhatIsInRightHand() != null)
                {
                    if (PublicFunctions.GetOriginalName(WhatIsInRightHand().name) == globalItemsList.items[2].item.name)
                    {
                        animator.SetTrigger("Axe Attack");
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
            if (childs[i].GetComponent<Item>())
            {
                result = childs[i].gameObject;
                break;
            }
        }
        return result;
    }

    public void OnStartOfAnim()
    {
        RaycastHit hit;
        if (Physics.Raycast(eyes.position, eyes.forward, out hit, 2f, layerMask))
        {
            target = hit.collider.gameObject;
            Debug.Log(target);
            targetDistanceAtStartOfAnimation = Vector3.Distance(target.transform.position, transform.position);
        }
        else
        {
            Debug.Log("Kein hit");
        }
    }

    public void OnHit()
    {
        if (target!=null)
        {
            if (Vector3.Distance(target.transform.position, transform.position)! <= targetDistanceAtStartOfAnimation)
            {
                Debug.Log("Blizko");
                if (target.GetComponent<Tree>())
                {
                    target.GetComponent<Tree>().RecivedHit(playersStats.strenght);
                }
            }
            else
            {
                Debug.Log("Daleko");
            }
        }
        
    }



}
