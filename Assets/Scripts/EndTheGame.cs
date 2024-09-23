using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTheGame : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(StarEndTheGame());
    }

    IEnumerator StarEndTheGame()
    {
        yield return new WaitForSeconds(30f);
        //Cargar Menu Principal
    }
}
