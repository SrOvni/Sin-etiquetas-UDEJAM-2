using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinTheGame : MonoBehaviour
{
    [SerializeField] public bool _sordo = false;
    [SerializeField] public bool _ciega = false;
    [SerializeField] public bool _silla = false;
    [SerializeField] public bool _asperger = false;
    [SerializeField] public bool _senora = false;

    [SerializeField] private UnityEvent OnWinTheGame;

    GameManager _gameManager;

    private void Start()
    {
        _gameManager = GetComponent<GameManager>();
    }
    private void Update()
    {
       if(_senora && _ciega && _asperger && _sordo && _silla)
        {
            StartCoroutine(StartDanceScene());
        }
    }

    IEnumerator StartDanceScene()
    {
        OnWinTheGame.Invoke();
        _sordo = false;
        _silla = false;
        _asperger = false;
        _senora = false;
        _ciega = false;
        yield return new WaitForSeconds(10f);
        _gameManager.ChangeSceneByName("FinalDance");
    }
}
