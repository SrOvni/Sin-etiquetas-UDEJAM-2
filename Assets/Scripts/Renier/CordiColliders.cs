using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CordiColliders : MonoBehaviour
{
    [SerializeField] GameObject collider1;
    [SerializeField] GameObject collider2;
    [SerializeField] GameObject collider3;
    [SerializeField] GameObject collider4;
    [SerializeField] GameObject collider5;
    bool alreadyinitialize = false;
    [SerializeField] UnityEvent OnProcess;

    public void InitializeGame()
    {
        if(!alreadyinitialize)
        {
            alreadyinitialize = true;
            collider1.SetActive(false);
            collider2.SetActive(false);
            collider3.SetActive(false);
            collider4.SetActive(false);
            collider5.SetActive(false);
            OnProcess?.Invoke();
        }
        
    }
}
