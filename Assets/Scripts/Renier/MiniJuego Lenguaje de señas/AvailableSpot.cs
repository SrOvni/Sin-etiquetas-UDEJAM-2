using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class AvailableSpot : MonoBehaviour
{
    public bool isAvailable = true;
    [SerializeField] private int order = -1;
    public int Order{get{return order;}set{order = value;}}
    [SerializeField] private int hasTheCard = -1;
    public int HasTheCard { get{return hasTheCard;}set{hasTheCard = value;}}
    private void Start() {
    }
    
}
