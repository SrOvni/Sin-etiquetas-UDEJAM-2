using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMission : MonoBehaviour
{
    [SerializeField] private bool _playerInRange = false;
    [SerializeField] private bool _theMissionIsCompleted = false;
    [SerializeField] private string _nameItemMission;
    [SerializeField] private InventorySystem _playerInventory;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") 
        {
            _playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _playerInRange = false;
        }
    }

    private void Update()
    {
        
    }

    public void StartMission()
    {

    }

    public void DontHaveItemMission()
    {

    }

    public void CompletedMision()
    {
        _theMissionIsCompleted = true;
    }

    public void CheckInventoryPlayer() 
    {
        for(int i =0; i < _playerInventory._inventoryItems.Count; i++)
        {
            if (_playerInventory._inventoryItems[i]._nameItem.ToLower() == _nameItemMission.ToLower())
            {
                CompletedMision();
                return;
            }
        }
    }
}
