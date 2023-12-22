using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    public GameObject InventoryViewport;
    public GameObject menuItemPrefab;
    public GameObject itemControlsPanel;

    private Inventory _testInv;

    public List<GameObject> menuItems = new List<GameObject>();

    private MenuItem _selectedItem;

    public float menuItemGap;

    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomItems();
    }
    public void LoadInventory()
    {
        MenuItem m;
        GameObject a;
        foreach (Item item in _testInv.items) 
        {
            a = Instantiate(menuItemPrefab);
            m = a.GetComponent<MenuItem>();
            m.item = item;
            m.menuItemText.text = item.ItemName;
            m.inventoryManager = this;

            a.transform.SetParent(InventoryViewport.transform, false);
            menuItems.Add(a);
        }
    }

    private void FixedUpdate()
    {
        if(_testInv != null)
        {
            RectTransform rt;
            for(int i = 0; i < menuItems.Count; i++) 
            {
                if (menuItems[i].GetComponent<MenuItem>().isDragging) return;

                rt = menuItems[i].GetComponent<RectTransform>();
                float yPos = i * menuItemPrefab.GetComponent<RectTransform>().sizeDelta.y * menuItemGap;
                rt.localPosition = new Vector3(0, -yPos, 0);
            }
        }
    }

    public void GenerateRandomItems()
    {
        _testInv = new();
        Item item = new();
        for (int i = 0; i < 50; i++)
        {
            item = new();
            item.ItemName = Constants.ADJECTIVES[Random.Range(0, Constants.ADJECTIVES.Length)] + " " + Constants.ITEMNAMES[Random.Range(0, Constants.ITEMNAMES.Length)];
            item.RollRandomStats(3);
            _testInv.items.Add(item);
        }
        LoadInventory();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventory(_testInv);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GenerateRandomItems();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (_selectedItem == null) return;

            _selectedItem.gameObject.GetComponent<Image>().color /= 2;
            _selectedItem = null;
            ToggleControls();
        }
    }

    public void SelectItem(MenuItem item)
    {
        if(_selectedItem != null)
        {
            //set old selected item back to normal color
            _selectedItem.gameObject.GetComponent<Image>().color /= 2;
        } else
        {
            ToggleControls();
        }
        _selectedItem = item;
        _selectedItem.gameObject.GetComponent<Image>().color *= 2;
    }

    public void ToggleControls()
    {
        //toggles the actions for the item as it is selected

        //itemControlsPanel.SetActive(!itemControlsPanel.activeSelf);

    }


    public void ToggleInventory(Inventory inv)
    {
        
    }
}
