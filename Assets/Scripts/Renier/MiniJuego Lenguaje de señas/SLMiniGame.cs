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
    bool canvasTurnOff = false;
    private void Start() {
        


    }
    public void InitializeGame()
    {
        if(!playerWin)
        {
            OrdenarPosiciones();
            movement.DontMovePlayer();
            positions = GetPositions(groupOfPosition);
            words = GetPositions(groupWithWords);
            availablePosition = new List<bool>(positions.Count);
            availablePositionCount = positions.Count;
            startGame = true;
            StartCoroutine(StartGame());
        }else{
            WinGame?.Invoke();
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
                playerWin = true;
            }else{
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
    movement.CantMovePlayer();
    StartCoroutine(ReordenarCartas(secondsForCardToReorder));
    yield return new WaitUntil(()=>cartasOrdenadas);
    yield return new WaitUntil(()=> canvasTurnOff);
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
