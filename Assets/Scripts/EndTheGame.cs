using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTheGame : MonoBehaviour
{
    GameManager gameManager;
    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        StartCoroutine(StarEndTheGame());
    }
    private IEnumerator StarEndTheGame()
    {
        yield return new WaitForSeconds(30f);
        gameManager.ChangeSceneByName("MenuPrincipal");
    }
}
