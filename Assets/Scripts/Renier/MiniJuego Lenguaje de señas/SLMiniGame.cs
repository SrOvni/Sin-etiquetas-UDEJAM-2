using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SLMiniGame : MonoBehaviour
{
    [SerializeField] private  MovementPlayer movement;
    [SerializeField] GameObject groupOfPosition;
    [SerializeField] GameObject groupWithWords;
    [SerializeField] List<Transform> positions;
    [SerializeField] List<Transform> words;
    [SerializeField] bool startGame;
    [SerializeField] Timer timer;
    [SerializeField] TextMeshProUGUI win;
    [SerializeField] TextMeshProUGUI lose;
    [SerializeField] List<bool> availablePosition;
    [SerializeField] int availablePositionCount;
    [SerializeField] bool playerWin;
    public UnityEvent OnReturnPosition;
    [SerializeField] float secondsForCardToReorder = 2;
    [SerializeField] UnityEvent InReorderOfCards;
    [SerializeField] UnityEvent LoseGame;
    [SerializeField] UnityEvent WinGame;
    [SerializeField] UnityEvent OnComplete;
    [SerializeField] bool cartasOrdenadas;
    [SerializeField] float timeToTurnOffWinOrLoseCanvas;
    [SerializeField] GameObject instructions;
    bool canvasTurnOff = false;
    [SerializeField] WinTheGame winTheGame;
    [SerializeField] UnityEvent OnGameStart;
    [SerializeField] UnityEvent OnGameEnd;
    bool entendioInstrucciones = false;
    public void EntendioInstrucciones()
    {
        entendioInstrucciones = true;
    }
    private void Start() {
        

    }
    
    public void InitializeGame()
    {
        if(!playerWin)
        {
            OnGameStart?.Invoke();
            StartCoroutine(StartGame());
        }else{
            gameObject.SetActive(false);
            return;
        }
    }
    private void Update() {
    if(startGame)
    {
        if(availablePositionCount == 0)
        {
            if(CorrectPositions())
            {
                winTheGame._sordo = true;
                WinGame?.Invoke();
                playerWin = true;
            }else{
                //Incorrect positions;
                StartCoroutine(ReordenarCartas(secondsForCardToReorder));
            }
        }
        if(timer.CurrentTime <= 0)
        {
            playerWin = false;
            StartCoroutine(EndGame());
        }
    }
}
IEnumerator CloseCanvasWin()
{
    win.gameObject.SetActive(true);
    yield return new WaitForSeconds(secondsForCardToReorder);
    win.gameObject.SetActive(false);
    canvasTurnOff = true;
}
IEnumerator CloseCanvasLose()
{
    lose.gameObject.SetActive(true);
    yield return new WaitForSeconds(secondsForCardToReorder);
    lose.gameObject.SetActive(false);
    canvasTurnOff = true;
}
IEnumerator EndGame()
{
    entendioInstrucciones = false;
    if(playerWin)
    {
        StartCoroutine(CloseCanvasWin());
        WinGame?.Invoke();
    }
    else
    {
        StartCoroutine(CloseCanvasLose());
        LoseGame?.Invoke();
    }
    timer.start = false;
    timer.RestarTimer();
    startGame = false;
    StartCoroutine(ReordenarCartas(secondsForCardToReorder));
    yield return new WaitUntil(()=>cartasOrdenadas);
    yield return new WaitUntil(()=> canvasTurnOff);
    OnGameEnd?.Invoke();
    movement.CantMovePlayer();
    gameObject.SetActive(false);
}
    private List<Transform> GetPositions(GameObject group)
    {
        List<Transform> positions = new List<Transform>();
        for (int i = 0; i < group.transform.childCount; i++)
        {
            positions.Add(group.transform.GetChild(i));
        }
        return positions;
    }
    void OrdenarPosiciones()
    {
        for (int i = 0; i < positions.Count;i++)
        {
            positions[i].GetComponent<AvailableSpot>().Order = i;
        }
    }
    bool CorrectPositions()
    {
        int count = 0;
        for(int i = 0; i < positions.Count; i++)
        {
            if(positions[i].GetComponent<AvailableSpot>().Order == positions[i].GetComponent<AvailableSpot>().HasTheCard)
            {
                count++;
            }
        }
        if(count == positions.Count)
        {
            return true;
        }else{
            return false;
        }
    }
    IEnumerator StartGame()
    {
            instructions.SetActive(true);
            yield return new WaitUntil(()=> entendioInstrucciones);
            instructions.SetActive(false);
            canvasTurnOff = false;
            movement.DontMovePlayer();
            positions = GetPositions(groupOfPosition);
            words = GetPositions(groupWithWords);
            availablePosition = new List<bool>(positions.Count);
            availablePositionCount = positions.Count;
            OrdenarPosiciones();
            startGame = true;
        yield return new WaitUntil(()=>startGame);
        timer.start = true;
        yield return new WaitUntil(()=>playerWin);
        yield return new WaitForSeconds(1);
        StartCoroutine(EndGame());
    }
    public void AgregarCarta()
    {
        availablePositionCount--;
    }
    public  void RetirarCarta()
    {
        availablePositionCount++;
    }
    IEnumerator ReordenarCartas(float secondsToReoder)
    {
        yield return new WaitForSeconds(secondsToReoder);
        OnReturnPosition?.Invoke();
        cartasOrdenadas = true;

    }
}
