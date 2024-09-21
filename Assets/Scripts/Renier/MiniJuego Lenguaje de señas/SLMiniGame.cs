using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLMiniGame : MonoBehaviour
{
    [SerializeField] GameObject groupOfAvailablePosition;
    [SerializeField] GameObject groupWithWords;
    [SerializeField] List<Transform> availablePositions;
    [SerializeField] List<Transform> words;
    [SerializeField] bool startGame;
    private void Start() {
        availablePositions = GetPositions(groupOfAvailablePosition);
        words = GetPositions(groupWithWords);
    }
private void Update() {
    
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
}
