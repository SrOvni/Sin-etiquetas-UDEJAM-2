using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SignLanguageMiniGame : MonoBehaviour
{
    [SerializeField] List<SignType> RondaUno;  
    public List<Transform> positions;
    public List<bool> availablePositions;
    AvailableSpot availableSpot;
    int index = 0;
    int belongsTo = -1;

    private void Start() {
        availableSpot = GetComponent<AvailableSpot>();
        GetPositions();
        availablePositions = new List<bool>(positions.Count);
        for (int i = 0; i < positions.Count; i++) {
            availablePositions.Add(true);
        }
    }
    public Vector2 NextPositonAvailable()
    {
        Vector2 newPosition = Vector2.zero;
        for (int i = 0; i < positions.Count; i++)
        {
            if(availablePositions[i])
            {
                newPosition = positions[i].localPosition;
                availablePositions[i] = false;
                belongsTo = i;
                break;
            }
        }
        return newPosition;
    }
    public int WhereIsPositioned()
    {
        return belongsTo;
    }
    public void ReturnCard(int index)
    {
        if (index >= 0 && index < availablePositions.Count) {
            availablePositions[index] = true;
        }
    }

    void GetPositions()
    {
        for(int i = 0; i < transform.childCount;i++)
        {
            positions.Add(transform.GetChild(i));
            positions[i].TryGetComponent(out AvailableSpot component);
            component.belongsTo = i;
        }
    }
}
