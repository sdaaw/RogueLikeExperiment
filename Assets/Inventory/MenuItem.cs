using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour
{
    // Start is called before the first frame update


    public Item item;
    public GameObject toolTipObject;

    public TMP_Text menuItemText;

    private GameObject _activeTooltip;
    private RectTransform _toolTipTransform;

    public Canvas canvas;

    public InventoryManager inventoryManager;

    private float toolTipOffsetX, toolTipOffsetY;

    public bool isDragging;

    private RectTransform _rtransform;

    void Start()
    {
        _rtransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_toolTipTransform != null && !isDragging) 
        {
            Vector3 mPos = Input.mousePosition;
            mPos = new Vector3(mPos.x + toolTipOffsetX, mPos.y + toolTipOffsetY, 0);
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(_rtransform, mPos, Camera.main, out Vector2 pos);
            _toolTipTransform.position = mPos;
        }
    }

    public void OnHover()
    {
        if(_toolTipTransform != null || _activeTooltip.activeSelf) //if item has its tooltip already instantiated, set it active and return, or if it already is active.
        {
            _activeTooltip.SetActive(true);
            return;
        }
        //either edit a single tooltip object or instantiate a different tooltip for each item in the menu
        //cpu vs memory? idk, which one is more expensive, setting gameobject active or editing text in an object
        //this feels nice too, until you have 34059345 items ofc.
        _activeTooltip = Instantiate(toolTipObject);
        _toolTipTransform = _activeTooltip.GetComponent<RectTransform>();
        _toolTipTransform.SetParent(canvas.transform, false);

        if(_toolTipTransform.sizeDelta.x != 0 || _toolTipTransform.sizeDelta.y != 0) //prevent Unity from crashing 
        {
            toolTipOffsetX = _toolTipTransform.sizeDelta.x / 2;
            toolTipOffsetY = _toolTipTransform.sizeDelta.y / 2;
        }
        //to prevent the mouse from entering on top of the tooltip, blocking the hover event by unity event system, causing flickering
        toolTipOffsetX += 1;
        toolTipOffsetY += 1;

        //build contents of the tooltip and set the position it on mouse
        string displayText = item.ItemName + "\n" + item.Description;
        _toolTipTransform.GetComponent<MenuTooltip>().TooltipText.text = displayText;
        Vector3 mPos = Input.mousePosition;
        mPos = new Vector3(mPos.x + toolTipOffsetX, mPos.y + toolTipOffsetY, 0);
        _toolTipTransform.position = mPos;
    }
    public void OnStopHover()
    {
        _activeTooltip.SetActive(false);
    }
    public void OnClick()
    {
        if (isDragging) return;
        inventoryManager.SelectItem(this);
    }
}
