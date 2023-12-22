using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour
{
    // Start is called before the first frame update


    public Item item;
    public GameObject toolTipObject;

    public TMP_Text menuItemText;

    private GameObject _activeTooltip;
    private RectTransform _toolTipTransform;

    private Canvas _canvas;

    public InventoryManager inventoryManager;
    private float toolTipOffsetX, toolTipOffsetY;

    void Start()
    {
        toolTipOffsetX = 160;
        toolTipOffsetY = -70;
        _canvas = FindFirstObjectByType<Canvas>(); //
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_toolTipTransform != null) 
        {
            Vector3 mPos = Input.mousePosition;
            mPos = new Vector3(mPos.x + toolTipOffsetX, mPos.y + toolTipOffsetY, 0); //magic numbers, also if the tooltip appears in front of the mouse, it will flicker due to stopping the hover event on the actual item.
            _toolTipTransform.position = mPos;
        }
    }

    public void OnHover()
    {
        if(_toolTipTransform != null)
        {
            _activeTooltip.SetActive(true);
            return;
        }
        //either edit a single tooltip object or instantiate a different tooltip for each item in the menu
        //cpu vs memory? idk, which one is more expensive, setting gameobject active or editing text in an object
        //this feels nice too, until you have 34059345 items ofc.
        _activeTooltip = Instantiate(toolTipObject);
        _toolTipTransform = _activeTooltip.GetComponent<RectTransform>();
        _toolTipTransform.SetParent(_canvas.transform, false);
        string displayText = item.ItemName + "\n" + item.Description;
        _toolTipTransform.GetComponent<MenuTooltip>().TooltipText.text = displayText;
        Vector3 mPos = Input.mousePosition;
        mPos = new Vector3(mPos.x + toolTipOffsetX, mPos.y + toolTipOffsetY, -1);
        _toolTipTransform.position = mPos;
    }
    public void OnStopHover()
    {
        _activeTooltip.SetActive(false);
    }
    public void OnClick()
    {
        inventoryManager.SelectItem(this);
    }
}
