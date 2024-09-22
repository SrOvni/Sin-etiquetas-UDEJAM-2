using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPCMission : MonoBehaviour
{
    [SerializeField] public bool _theMissionIsCompleted = false;
    [SerializeField] public bool _missionIsStarted = false;
    [SerializeField] private InventoryItem _itemsToSearch;
    [SerializeField] private InventorySystem _playerInventory;

    [SerializeField] public UnityEvent OnCompleteMision;
    [SerializeField] public UnityEvent OnDontHaveItems;
    [SerializeField] public UnityEvent OnStartMission;

    public void StartMission()
    {
        _missionIsStarted = true;
        Debug.Log("Mision iniciada");
        OnStartMission.Invoke();
    }

    public void DontHaveItemMission()
    {
        OnDontHaveItems.Invoke();
        Debug.Log("No tienes el item");
    }

    public void CompletedMision()
    {
        _theMissionIsCompleted = true;
        Debug.Log("Completaste la mision");
        OnCompleteMision.Invoke();
    }

    public void CheckInventoryPlayer() 
    {
        if (_playerInventory._inventoryItems.Count <= 0 && _missionIsStarted)
        {
            DontHaveItemMission();
        }
        else
        {
            if (!_theMissionIsCompleted && _missionIsStarted)
            {
                for (int i = 0; i < _playerInventory._inventoryItems.Count; i++)
                {
                    if (_playerInventory._inventoryItems[i] == _itemsToSearch)
                    {
                        CompletedMision();
                        _playerInventory.RemoveItemByName(_itemsToSearch);
                    }
                    else
                    {
                        DontHaveItemMission();
                    }
                }
            }
            else
            {
                OnCompleteMision.Invoke();
            }
        }       
    }
}
