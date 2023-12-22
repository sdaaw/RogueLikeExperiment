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
                rt = menuItems[i].GetComponent<RectTransform>();
                float yPos = 0 + i * 20;
                rt.localPosition = new Vector3(0, -yPos, 0);
            }
        }
    }

    public void GenerateRandomItems()
    {
        _testInv = new();
        Item item;
        for (int i = 0; i < 50; i++)
        {
            item = new(true);
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
        }
    }

    public void SelectItem(MenuItem item)
    {
        if(_selectedItem != null)
        {
            _selectedItem.gameObject.GetComponent<Image>().color /= 2;
        }
        _selectedItem = item;
        _selectedItem.gameObject.GetComponent<Image>().color *= 2;
    }

    public void ToggleControls()
    {
        //toggles the actions for the item as it is selected

    }


    public void ToggleInventory(Inventory inv)
    {
        
    }
}
