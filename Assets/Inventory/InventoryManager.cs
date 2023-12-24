using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    public Canvas canvas;

    private Inventory _testInv;

    public List<GameObject> menuItems = new List<GameObject>();


    [SerializeField]
    private float menuItemGap;

    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomItems();
    }
    public void LoadInventory()
    {
        //set up and build the items for the UI

        MenuItem m;
        GameObject a;
        foreach (Item item in _currentInventory.items) 
        {
            a = Instantiate(MenuItemPrefab);
            m = a.GetComponent<MenuItem>();
            m.item = item;
            m.menuItemText.text = item.ItemName;
            m.inventoryManager = this;
            m.canvas = canvas;

            a.transform.SetParent(InventoryViewport.transform, false);
            menuItems.Add(a);
        }
    }

    private void FixedUpdate()
    {
        if(_currentInventory != null)
        {
            RectTransform rt;
            for(int i = 0; i < menuItems.Count; i++) 
            {
                if (menuItems[i].GetComponent<MenuItem>().isDragging) return;

                rt = menuItems[i].GetComponent<RectTransform>();
                float yPos = i * MenuItemPrefab.GetComponent<RectTransform>().sizeDelta.y * menuItemGap;
                rt.localPosition = new Vector3(0, -yPos, 0); //list goes downwards so its negative
            }
        }
    }

    public void GenerateRandomItems()
    {
        _currentInventory = new();
        Item item = new();
        for (int i = 0; i < 50; i++) 
        {
            item = new();
            item.ItemName = Constants.ADJECTIVES[Random.Range(0, Constants.ADJECTIVES.Length)] + " " + Constants.ITEMNAMES[Random.Range(0, Constants.ITEMNAMES.Length)];
            item.RollRandomStats(3);
            _currentInventory.items.Add(item);
        }
        LoadInventory();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventory();
        }
        if (Input.GetKeyDown(KeyCode.Space))
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

    public void ToggleInventory()
    {

    }

    public void SelectItem(MenuItem item)
    {
        if(_selectedItem != null)
        {
            //set old selected item back to normal color
            _selectedItem.gameObject.GetComponent<Image>().color /= 2;
        }
        _selectedItem = item;
        _selectedItem.gameObject.GetComponent<Image>().color *= 2;
    }
}
