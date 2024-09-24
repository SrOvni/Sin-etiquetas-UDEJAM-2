using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTheGame : MonoBehaviour
{   
    public IEnumerator StarEndTheGame()
    {
        yield return new WaitForSeconds(30f);
        //Cargar Menu Principal
    }
}
