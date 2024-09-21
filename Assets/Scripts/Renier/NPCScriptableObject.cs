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
    public string protaRejectionLine = "No puedo ahora, lo siento";
    public string npcsRejectionLine = "Espero que me puedas ayudar";
    public WhoIsTalking[] dialogueOrder;
    public bool[] isPlayerDecision;
    [Serializable] public struct PlayerDecisions
    {
        public string desicion1;
        public string desicion2;
    }
    public List<PlayerDecisions> playerDecisions;

}

