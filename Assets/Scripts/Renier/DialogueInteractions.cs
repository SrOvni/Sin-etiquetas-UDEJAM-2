using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class DialogueInteractions : MonoBehaviour
{
    InputManager _inputs;
    TemporalMovement movement;
    bool spaceBarWasPressed  = false;
    private void Start() {
        _inputs = GetComponent<InputManager>();
        movement = GetComponent<TemporalMovement>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.TryGetComponent(out NPCDialogues component))
        {
           component.SpaceBarImage.gameObject?.SetActive(true);
           component.ExclamationSign.gameObject.SetActive(false);
        }   
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.TryGetComponent(out NPCDialogues component))
        {
            if(_inputs.IsSpaceBarPressed && !spaceBarWasPressed)
            {
                movement.enabled = false;
                component.PlayDialogue();
                spaceBarWasPressed = true;
            }
            else if(_inputs.IsEscapeKeyPressed)
            {
                movement.enabled = true;
                component.ExitDialogue();
                spaceBarWasPressed = false;
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
