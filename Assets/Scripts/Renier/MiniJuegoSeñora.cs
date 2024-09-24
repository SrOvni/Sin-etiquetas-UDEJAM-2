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
    [SerializeField] private int requiredClicksPerPopUp = 1;
    public int RequiredClicksPerPopUp{get{return requiredClicksPerPopUp;}set{requiredClicksPerPopUp = value;}}
    [SerializeField] private  TextMeshProUGUI timerText;
    [SerializeField] private Button closeButton;
    [SerializeField] private float numberOfPopUps = 8;
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
    [SerializeField] WinTheGame winTheGame;
    [SerializeField] GameObject guardacomo;
    [SerializeField] GameObject pdfOn;
    [SerializeField] GameObject pdfOff;
    private bool botonPDFpresionado = false;
    public bool BotonPDFpresionado {get {return botonPDFpresionado;}set{botonPDFpresionado = value;}}
    bool canvasisoff;
    bool finishedpopingout = false;
    private float TimeToTurnOffCanvas;
    private bool gameEnded = false;
    private bool animacionTerminada = false;

    public void StartGame()
    {
        if(win){
            OnPlayerWin?.Invoke();
            return;
        }
        botonPDFpresionado = false;
        animacionTerminada = false;
        gameEnded = false;
        playerMovement.CantMovePlayer();
        timer.gameObject.SetActive(true);
        timer.start = true;
        startGame = true;
        canvasisoff = false;
        StartCoroutine(StartPopUpWindowGame());
    }
    private void Update() {
    if (startGame)
    {
        if (timer.CurrentTime <= 0|| finishedpopingout)
        {
            gameEnded = true;
            StartCoroutine(OnGameEnd());
        }
    }
}

    private IEnumerator WinOrLoseCanvas(bool win)
    {

        if (win)
        {
            StartCoroutine(AnimaciónGuardarComo());
            yield return new WaitUntil(() => animacionTerminada);
            OnPlayerWin?.Invoke();
            winnedGameText.SetActive(true);
            yield return new WaitForSeconds(1);
            winnedGameText.SetActive(false);
            canvasisoff = true;
            OnPlayerWin?.Invoke();
            winTheGame._senora = true;           
        }
        else{
            OnPlayerLose?.Invoke();
            losedgameText.SetActive(true);
            yield return new WaitForSeconds(1);
            losedgameText.SetActive(false);
            canvasisoff = true;
            OnPlayerLose?.Invoke();
        }
    }
    IEnumerator OnGameEnd(){       
        yield return new WaitUntil(()=>RestartGame());
        startGame = false;
        timer.start = false;
        timer.gameObject.SetActive(false);
        playerMovement.enabled = true;
        StartCoroutine(WinOrLoseCanvas(win));
        yield return new WaitUntil(()=> canvasisoff);
        timer.RestarTimer();
        mainWindow.SetActive(false);
    }
    IEnumerator StartPopUpWindowGame()
    {
        yield return new WaitUntil(()=> startGame);
        mainWindow.SetActive(true);
        for(int i = 0; i < popupWindowgroup.transform.childCount;i++)
        {
            if(gameEnded)break;
            popUps[i].GetComponent<PopUpWindow>().ShowWindow();
            yield return new WaitForSeconds(timeBetweenPopUps);
        }
        if(!gameEnded)
        {
            yield return new WaitUntil(()=> popUps.All(popup => popup.GetComponent<PopUpWindow>().WindowsIsClosed));
            win = true;
            finishedpopingout = true;
        }

    }
    bool RestartGame()
    {
        for(int i = 0; i< popupWindowgroup.transform.childCount;i++)
        {
            if(popUps[i].activeInHierarchy)
            {
                popUps[i].GetComponent<PopUpWindow>().RestartGame();
                popUps[i].gameObject.SetActive(false);
            }
        }
        return true;
    }
    IEnumerator AnimaciónGuardarComo()
    {
        guardacomo.SetActive(true);
        yield return new WaitUntil(()=> botonPDFpresionado);
        pdfOn.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        pdfOn.SetActive(true);
        animacionTerminada = true;

    }
}
