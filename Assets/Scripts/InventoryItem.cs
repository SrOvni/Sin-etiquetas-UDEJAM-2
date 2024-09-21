using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] public Sprite _itemImage;
    [SerializeField] public string _nameItem;

    [SerializeField] public UnityEvent OnTakeObject;

   public void OnTake()
    {
        this.gameObject.SetActive(false);
        OnTakeObject.Invoke();
    }
}
