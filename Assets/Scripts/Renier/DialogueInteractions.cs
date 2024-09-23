using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
public enum PlayerQuestState
{
    dialogue,
    winGame,
    restartGame,
    reDoQuest,
    bringItem
}
public class DialogueInteractions : MonoBehaviour
{
    [SerializeField] private PlayerQuestState playerQuestState = PlayerQuestState.dialogue;
    public PlayerQuestState CurrentPlayerQuestState{get{return playerQuestState;}set{playerQuestState = value;}}
    InputManager _inputs;
    [SerializeField] NPCScriptableObject fistTimeDialogue;
    [SerializeField] NPCScriptableObject retryQuest;
    [SerializeField] NPCScriptableObject reDoQuest;
    [SerializeField] NPCScriptableObject wrongItems;
    [SerializeField] NPCScriptableObject correctItems;

    public MovementPlayer Movement{get; private set;}
    [SerializeField] bool hasInteracted  = false;
    public bool HasInteracted{get{return hasInteracted;}set{hasInteracted = value;}}
    private void Start() {
        _inputs = GetComponent<InputManager>();
        Movement = GetComponent<MovementPlayer>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.TryGetComponent(out NPCDialogues component))
        {
            Debug.Log("Script found");
           component.SpaceBarImage.gameObject?.SetActive(true);
           component.ExclamationSign.gameObject.SetActive(false);
        }   
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.TryGetComponent(out NPCDialogues component))
        {
            if(_inputs.Interact && !hasInteracted)
            {
                hasInteracted = true; //Volver falso después de la interacción para poder volver a interactuar con los NPCs
                component.PlayDialogueQuest();
            }
        }   
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.TryGetComponent(out NPCDialogues component))
        {
                component.SpaceBarImage.gameObject?.SetActive(false);
                component.ExclamationSign.gameObject?.SetActive(true);

        }
    }
    IEnumerator NPCQuest()
    {
        Image spacebarCanva = gameObject.GetComponentInChildren<Image>();
        spacebarCanva.gameObject.SetActive(true);
        yield return null;
    }
}
