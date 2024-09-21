using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SLMiniGame : MonoBehaviour
{
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
    private void Start() {
        positions = GetPositions(groupOfPosition);
        words = GetPositions(groupWithWords);
        OrdenarPosiciones();
        availablePosition = new List<bool>(positions.Count);
        availablePositionCount = positions.Count;
        StartCoroutine(StartGame());
        timer.time = 50f;


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
        if(timer.CurrentTime == 0)
        {
            playerWin = false;
        }
    }else{
        playerWin = false;
    }
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
        startGame = false;
        timer.start = false;
        if(playerWin)
        {
            win.gameObject.SetActive(true);
        }else{
            lose.gameObject.SetActive(true);
        }
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
    }
}
