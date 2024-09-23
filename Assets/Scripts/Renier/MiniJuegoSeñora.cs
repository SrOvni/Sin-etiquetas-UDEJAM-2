using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MiniJuegoSeñora : MonoBehaviour
{
    [SerializeField] private MovementPlayer playerMovement;
    [SerializeField] private GameObject mainWindow;
    [SerializeField] private GameObject popupWindowgroup;
    [SerializeField] private List<GameObject> popUps;
    [SerializeField] private GameObject positions;
    [SerializeField] private Transform[] randomPosition;
    public Transform[] RandomPositions{get{return randomPosition;}set{randomPosition = value;}}
    [SerializeField] private bool startGame = false;
    public bool StartGame{get{return startGame;}set{startGame = value;} }
    [SerializeField] private int requiredClicksPerPopUp = 1;
    public int RequiredClicksPerPopUp{get{return requiredClicksPerPopUp;}set{requiredClicksPerPopUp = value;}}
    [SerializeField] private  TextMeshProUGUI timerText;
    [SerializeField] private Button closeButton;
    [SerializeField] private float numberOfPopUps = 5;
    public float NumberOfPopUps{get{return numberOfPopUps;}set{numberOfPopUps = value;}}
    [SerializeField] private float timeBetweenPopUps = 1;
    [SerializeField] private float timeToWin = 10;
    [SerializeField] private GameObject winnedGameText;
    [SerializeField] private GameObject losedgameText;
    [SerializeField] private GameObject miniGameCanvas;
    [SerializeField] private Renderer popUpRenderer;
    [SerializeField] Timer timer;
    [SerializeField] private bool win = false;
    [SerializeField] UnityEvent OnPlayerWin;
    [SerializeField] UnityEvent OnPlayerLose;
    [SerializeField] bool waiting = true;
    bool finishedpopingout = false;

    private void Start() {
        StartCoroutine(StartPopUpWindowGame());
        Debug.Log("Corrutina iniciada");
        timer.CurrentTime = timeToWin;
        timer.time = timeToWin;
    }

    private void Update() {
    if (startGame)
    {
        waiting = false;
        timer.start = true;

        if (timer.CurrentTime <= 0|| finishedpopingout)
        {
            timer.start = false;
            startGame = false;
            StartCoroutine(WinOrLoseCanvas(win));
        }
    }
}

    private IEnumerator WinOrLoseCanvas(bool win)
    {

        if (win)
        {
            OnPlayerWin?.Invoke();
            winnedGameText.SetActive(true);
            yield return new WaitForSeconds(1);
            winnedGameText.SetActive(false);
            
        }else{
            OnPlayerLose?.Invoke();
            losedgameText.SetActive(true);
            yield return new WaitForSeconds(1);
            losedgameText.SetActive(false);
        }
        OnGameEnd();
    }
    void OnGameStart()
    {
        playerMovement.enabled = false;
        timer.gameObject.SetActive(true);
        //Falta quitar velocity de rigidbody
    }
    void OnGameEnd(){
        timer.gameObject.SetActive(false);
        mainWindow.SetActive(false);
        playerMovement.enabled = true;
        StopAllCoroutines();
    }
    IEnumerator StartPopUpWindowGame()
    {
        yield return new WaitUntil(()=> startGame);
        mainWindow.SetActive(true);
        Debug.Log("Game Started");
        OnGameStart();
        for(int i = 0; i < popupWindowgroup.transform.childCount;i++)
        {

            popUps[i].GetComponent<PopUpWindow>().ShowWindow();
            yield return new WaitForSeconds(3);
        }
        yield return new WaitUntil(()=> popUps.All(popup => popup.GetComponent<PopUpWindow>().WindowsIsClosed));
        win = true;
        finishedpopingout = true;

    }
}
