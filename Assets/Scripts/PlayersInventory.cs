using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayersInventory : MonoBehaviour
{
    public double maxCapacity = 10;
    public double usedCapacity = 0;
    public double usedWeight = 0;

    public TMP_Text capacity;
    public TMP_Text weight;
    public GameObject UI;
    public GameObject back;
    public GameObject right;
    public Transform eyes;
    public Transform smallSLots;
    public PlayerStats playerStats;
    private List<Image> images = new List<Image>();

    private Transform inventory;
    public TMP_Text hints;
    public LayerMask layerMask;

    public GameObject slotPrefab;
    public ItemsList itemsList;

    public GameObject Options;
    private GameObject selected;
    private Button drop;
    private Button consume;

    private int x = 0;
    private int y = 0;

    public Transform RHand;
    public Transform PlayersBack;

    public Animator animator;

    public bool isOpen = false;

    public TMP_Text info;
    void Start()
    {
        playerStats = gameObject.GetComponent<PlayerStats>();
        inventory = transform.Find("Inventory");
        itemsList = GameObject.Find("Public servicies").GetComponent<ItemsList>();
        Options = UI.transform.Find("Options").gameObject;
        drop = Options.transform.Find("DropBTN").GetComponent<Button>();
        drop.onClick.AddListener(OnDrop);
        consume = Options.transform.Find("ConsumeBTN").GetComponent<Button>();
        consume.onClick.AddListener(OnConsume);
        SetInventoryUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (UI.active)
            {
                UI.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                isOpen = false;
            }
            else
            {
                UI.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                isOpen = true;
            }
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f, layerMask))
        {
            if (hit.collider.gameObject.GetComponent<Item>())
            {               
                if (hit.collider.gameObject.GetComponent<Item>().size+usedCapacity<=maxCapacity && hit.collider.gameObject.GetComponent<Item>().weight+usedWeight<=playerStats.strenght)
                {
                    hints.text = "C - vzít";
                    if (Input.GetKeyDown(KeyCode.C))
                    {
                        PickSmall(hit.collider.gameObject);
                    }
                    
                }
                else if (hit.collider.gameObject.GetComponent<Item>().weight + usedWeight <= playerStats.strenght && (right.GetComponent<Image>().sprite == null|| back.GetComponent<Image>().sprite == null))
                {
                    if (right.GetComponent<UIInventorySlot>().objekt == null)
                    {
                        hints.text = "C - vzít";
                        if (Input.GetKeyDown(KeyCode.C))
                            PickLarge(hit.collider.gameObject);
                    }
                    else if (back.GetComponent<UIInventorySlot>().objekt == null)
                    {
                        hints.text = "C - vzít";
                        if (Input.GetKeyDown(KeyCode.C))
                            PickLarge(hit.collider.gameObject);
                    }
                }
                else
                {
                    hints.text = "Nedostatek místa nebo pøetížení";
                }
                
            }
        }
    }
    private void SetCapacityUI()
    {
        capacity.text = (usedCapacity.ToString()+"/"+maxCapacity.ToString()+" Místa využito");
    }
    private void SetWeightUI()
    {
        weight.text = (usedWeight.ToString()+"/"+playerStats.strenght.ToString()+" Zatížení využito");
    }
    private void SetInventoryUI()
    {
        SetCapacityUI();
        SetWeightUI();
    }
    public void PickSmall(GameObject a)
    {
        a.transform.parent = inventory.transform;
        a.GetComponent<Rigidbody>().useGravity = false;
        MeshRenderer[] meshRenderers = a.GetComponentsInChildren<MeshRenderer>();
        foreach (var item in meshRenderers)
        {
            item.GetComponent<MeshRenderer>().enabled = false;
        }
        a.GetComponent<Collider>().enabled = false;
        
        usedCapacity += a.GetComponent<Item>().size;
        usedWeight += a.GetComponent<Item>().weight;
        Sprite sprite = itemsList.items[0].sprite;
        foreach (var item in itemsList.items)
        {
            if (item.item.name == PublicFunctions.GetOriginalName(a.name))
            {
                sprite = item.sprite;
                break;
            }
        }
        GameObject slot = GameObject.Instantiate(slotPrefab);
        slot.GetComponent<UIInventorySlot>().Sprite = sprite;
        slot.GetComponent<UIInventorySlot>().objekt = a;
        slot.transform.parent = smallSLots;
        slot.transform.localPosition = new Vector3(-282+(55*x),100 + (55 * y),0);
        slot.GetComponent<UIInventorySlot>().clicked += OnClicked;
        if (x == 10)
        {
            x = 0;
            y++;
        }
        else {
            x++;
        }
        SetInventoryUI();
    }
    private void PickLarge(GameObject a)
    {
        usedWeight += a.GetComponent<Item>().weight;
        Collider[] colliders = a.GetComponentsInChildren<Collider>();
        foreach (var item in colliders)
        {
            item.enabled = false;
        }
        a.GetComponent<Rigidbody>().useGravity = false;
        
        Sprite sprite = itemsList.items[0].sprite;
        Vector3 rpos = Vector3.zero;
        Vector3 rrot = Vector3.zero;
        Vector3 bpos = Vector3.zero;
        Vector3 brot = Vector3.zero;
        foreach (var item in itemsList.items)
        {
            if (item.item.name == PublicFunctions.GetOriginalName(a.name))
            {
                sprite = item.sprite;
                rpos = item.rpos;
                rrot = item.rrot;
                bpos = item.bpos;
                brot = item.brot;
                if (a.tag == "Rifle")
                {
                    animator.SetBool("HasRifle", true);
                }
                break;
            }
        }
        if (right.GetComponent<Image>().sprite==null)
        {
            a.transform.parent = RHand.transform;
            right.GetComponent<Image>().sprite = sprite;
            a.transform.localPosition = rpos;
            a.transform.localEulerAngles = rrot;
            right.GetComponent<UIInventorySlot>().clicked += OnClicked;
            right.GetComponent<UIInventorySlot>().objekt = a;

        }
        else if (back.GetComponent<Image>().sprite == null)
        {
            a.transform.parent = PlayersBack.transform;
            back.GetComponent<Image>().sprite = sprite;
            a.transform.localPosition = bpos;
            a.transform.localEulerAngles = brot;
            back.GetComponent<UIInventorySlot>().clicked += OnClicked;
            back.GetComponent<UIInventorySlot>().objekt = a;
        }
    }
    private void OnClicked(GameObject a)
    {
        Options.SetActive(true);
        //TMP_Text info = Options.GetComponentInChildren<TMP_Text>();
        foreach (var item in itemsList.items)
        {
            if (item.item.name == PublicFunctions.GetOriginalName(a.GetComponent<UIInventorySlot>().objekt.name))
            {
                if (a.GetComponent<UIInventorySlot>().objekt.GetComponent<Item>().food<1 && a.GetComponent<UIInventorySlot>().objekt.GetComponent<Item>().drink <1)
                {
                    consume.gameObject.SetActive(false);
                    Debug.Log("nešlo nic");
                    info.text = a.GetComponent<UIInventorySlot>().objekt.GetComponent<Magazine>().ammo + "/30";
                    consume.gameObject.SetActive(false);
                }
                else
                {

                    if (a.GetComponent<UIInventorySlot>().objekt.GetComponent<Item>().food > 0)
                    {
                        Debug.Log(a.GetComponent<UIInventorySlot>().objekt + " má jídlo tpè");
                        info.text = "Jídlo + " + a.GetComponent<UIInventorySlot>().objekt.GetComponent<Item>().food;
                        consume.gameObject.SetActive(true);
                    }
                    else if (a.GetComponent<UIInventorySlot>().objekt.GetComponent<Item>().drink > 0)
                    {
                        info.text = "Pití + " + a.GetComponent<UIInventorySlot>().objekt.GetComponent<Item>().drink;
                        consume.gameObject.SetActive(true);
                    }
                    else
                    {
                        
                    }
                }
                break;
            }
        }
        selected = a;
    }
    private void OnDrop()
    {
        GameObject objekt = selected.GetComponent<UIInventorySlot>().objekt;
        objekt.transform.parent = transform;
        objekt.transform.position = transform.position;
        objekt.transform.localPosition.Set(1f, 1f, 1f);
        objekt.GetComponent<Rigidbody>().useGravity = true;
        objekt.GetComponent<MeshRenderer>().enabled = true;
        objekt.GetComponent<MeshCollider>().enabled = true;
        objekt.transform.parent = null;
        usedCapacity -= objekt.GetComponent<Item>().size;
        usedWeight -= objekt.GetComponent<Item>().weight;
        Destroy(selected);
        if(x==0)
        {
            x = 10;
            y--;
        }else { x--; }
        /*PreskladejUI();
        SetInventoryUI();*/
    }
    private void OnConsume()
    {
        GameObject objekt = selected.GetComponent<UIInventorySlot>().objekt;
        if (playerStats.hunger+objekt.GetComponent<Item>().food>100)
        {
            playerStats.hunger = 100;
        }
        else
        {
            playerStats.hunger += objekt.GetComponent<Item>().food;
        }
        if (playerStats.thirst + objekt.GetComponent<Item>().drink > 100)
        {
            playerStats.thirst = 100;
        }
        else
        {
            playerStats.thirst += objekt.GetComponent<Item>().drink;
        }
        
        Destroy(objekt);
        Destroy(selected);
    }
    /*private void PreskladejUI()
    {
        UIInventorySlot[] sloty = smallSLots.GetComponentsInChildren<UIInventorySlot>();
        Debug.Log(sloty.Length);
        int a = 0;
        int b = 0;
        foreach(UIInventorySlot s in sloty)
        {
            Debug.Log(s.gameObject);
            s.gameObject.transform.localPosition = new Vector3(-282 + (55 * a), 100 + (55 * b), 0);
            if (a == 10)
            {
                a = 0;
                b++;
            }
            else
            {
                a++;
            }
        }
    }*/
}
