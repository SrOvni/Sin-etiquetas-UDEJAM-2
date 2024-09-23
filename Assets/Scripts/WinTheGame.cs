using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTheGame : MonoBehaviour
{
    [SerializeField] public bool _sordo = false;
    [SerializeField] public bool _ciega = false;
    [SerializeField] public bool _silla = false;
    [SerializeField] public bool _asperger = false;
    [SerializeField] public bool _senora = false;

    private void Update()
    {
       if(_senora && _ciega && _asperger && _sordo && _silla)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        //Cargar nueva escena
    }
}
