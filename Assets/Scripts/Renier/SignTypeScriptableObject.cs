using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SignType
{
    Hola,
    Como,
    Estas,
    Hay,
    Una,
    Fiesta,
    Más,
    Tarde,
    Te,
    Gustaría,
    Venir
}
[CreateAssetMenu(fileName = "SignTypeScriptableObject", menuName = "SignTypeScriptableObject", order = 0)]
public class SignTypeScriptableObject : ScriptableObject {
    [Serializable]
    public struct SignTypeList
    {
        public List<SignType> newList;
    }
    public List<SignTypeList> Conversation = new List<SignTypeList>(); 
    
}