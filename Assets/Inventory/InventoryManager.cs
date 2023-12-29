using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    public Canvas canvas;

    [SerializeField]
    private GameObject InventoryPrefab, MenuItemPrefab;

    [SerializeField]
    private GameObject ContentParent;

    private Inventory _currentInventory;
    private GameObject _currentInventoryObject;

    private MenuItem _selectedItem;

    [SerializeField]
    private List<GameObject> _menuItems = new();


    [SerializeField]
    private float _menuItemGap;

    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomItems();
    }
    public void LoadInventory()
    {
        float x = Screen.width / 2;
        float y = Screen.height / 2;
        _currentInventoryObject = Instantiate(InventoryPrefab, new Vector3(x, y, 0), Quaternion.identity);
        _currentInventoryObject.transform.SetParent(canvas.transform);
        ContentParent = _currentInventoryObject.transform.GetChild(0).GetChild(0).gameObject; // Content transform
        foreach (Item item in _currentInventory.items) 
        {
            GameObject menuObject = Instantiate(MenuItemPrefab);
            MenuItem menuItem = menuObject.GetComponent<MenuItem>();
            menuItem.itemImage = menuObject.GetComponent<Image>();
            menuItem.menuItemText.text = item.ItemName;
            menuItem.inventoryManager = this;
            menuItem.canvas = canvas;
            menuItem.item = item;

            
            menuObject.transform.SetParent(ContentParent.transform, false);
            _menuItems.Add(menuObject);
        }
    }


    private void UpdateInventoryView()
    {
        if (_currentInventory != null)
        {
            RectTransform rt;
            for (int i = 0; i < _menuItems.Count; i++)
            {
                if (_menuItems[i].GetComponent<MenuItem>().isDragging) return;

                rt = _menuItems[i].GetComponent<RectTransform>();
                float yPos = i * MenuItemPrefab.GetComponent<RectTransform>().sizeDelta.y * _menuItemGap;
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

            print(item.Description + ", " + item.ItemName);
            _currentInventory.items.Add(item);
        }
        LoadInventory();
    }


    void Update()
    {
        UpdateInventoryView();
        if(Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventory();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateRandomItems();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            DeleteItem(_selectedItem);
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
        _currentInventoryObject.SetActive(!_currentInventoryObject.activeSelf);
    }

    private void DeleteItem(MenuItem menuItem)
    {
        if(menuItem == null) return;

        _menuItems.Remove(menuItem.gameObject);

        Destroy(menuItem.activeTooltip);
        Destroy(menuItem.gameObject);
    }

    public void SelectItem(MenuItem item)
    {
        if(_selectedItem != null)
        {
            //set old selected item back to normal color
            _selectedItem.itemImage.color /= 2;
        }
        _selectedItem = item;
        _selectedItem.itemImage.color *= 2;
    }
}
