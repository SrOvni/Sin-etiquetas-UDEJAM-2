using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndTheGame : MonoBehaviour
{   
    GameManager gameManager;

    [SerializeField] private UnityEvent _musicFinal;
    private void Start()
    {
        gameManager = GetComponent<GameManager>();
    }
    public IEnumerator StarEndTheGame()
    {
        _musicFinal.Invoke();
        yield return new WaitForSeconds(30f);
        gameManager.ChangeSceneByName("MenuPrincipal");
    }
}
