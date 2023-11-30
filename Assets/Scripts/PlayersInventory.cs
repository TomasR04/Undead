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

    private int x = 0;
    private int y = 0;
    void Start()
    {
        playerStats = gameObject.GetComponent<PlayerStats>();
        inventory = transform.Find("Inventory");
        itemsList = GameObject.Find("Public servicies").GetComponent<ItemsList>();
        Options = UI.transform.Find("Options").gameObject;
        drop = Options.transform.Find("DropBTN").GetComponent<Button>();
        drop.onClick.AddListener(OnDrop);
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
            }
            else
            {
                UI.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
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
                else if (hit.collider.gameObject.GetComponent<Item>().weight + usedWeight <= playerStats.strenght)
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
    private void PickSmall(GameObject a)
    {
        a.transform.parent = inventory.transform;
        a.GetComponent<Rigidbody>().useGravity = false;
        a.GetComponent<MeshRenderer>().enabled = false;
        a.GetComponent<MeshCollider>().enabled = false;
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
    }
    private void OnClicked(GameObject a)
    {
        Options.SetActive(true);
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
        PreskladejUI();
        SetInventoryUI();
    }
    private void PreskladejUI()
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
    }
}
