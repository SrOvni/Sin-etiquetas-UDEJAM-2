using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WhoIsTalking
{
    Prota,
    NPC
}
[CreateAssetMenu(fileName = "NPCScriptableObject", menuName = "NPCScriptableObject", order = 0)]
public class NPCScriptableObject : ScriptableObject 
{
    public string[] dialogues = new string[] {"No hay dialogo"};
    public string exitDialogue;
    public WhoIsTalking[] dialogueOrder;

}

