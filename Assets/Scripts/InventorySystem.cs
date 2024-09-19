using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventorySystem : MonoBehaviour
{
    public List<InventoryItem> _inventoryItems;

    public void AddItem(InventoryItem item)
    {
        _inventoryItems.Add(item);
    }

    public void RemoveItemByName(InventoryItem itemName)
    {
        foreach(InventoryItem item in _inventoryItems)
        {
            if(item._nameItem.ToLower() == itemName._nameItem.ToLower())
            {
                _inventoryItems.Remove(itemName);
                return;
            }
        }
    }

    public void UpdateUI()
    {

    }

}
