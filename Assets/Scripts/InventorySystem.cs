using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.Events;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] public List<InventoryItem> _inventoryItems;

    public GameObject panel;

    [SerializeField] public UnityEvent OnAddItem;

    [SerializeField] public UnityEvent OnRemoveItem;

    [SerializeField] public UnityEvent OnUpdateUI;

    public void AddItem(InventoryItem item)
    {
        _inventoryItems.Add(item);
        UpdateUI();
    }

    public void RemoveItemByName(InventoryItem itemName)
    {
        foreach(InventoryItem item in _inventoryItems)
        {
            if(item == itemName)
            {
                _inventoryItems.Remove(itemName);
                UpdateUI();
                return;
            }
        }
    }

    public void UpdateUI()
    {       
        foreach (Transform child in panel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (InventoryItem item in _inventoryItems)
        {           
            GameObject newImageObject = new GameObject("DynamicImage");
            
            Image newImage = newImageObject.AddComponent<Image>();

            newImage.sprite = item._itemImage;

            
            RectTransform rectTransform = newImage.GetComponent<RectTransform>();
            rectTransform.SetParent(panel.transform);  
            rectTransform.localScale = Vector3.one;    
            rectTransform.sizeDelta = new Vector2(150, 150);  
        }
    }
}
