using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInputs playerinputs;

    public Vector2 MovementDirection {get; private set;}
    public bool IsSpaceBarPressed{get; private set;}
    public bool IsEscapeKeyPressed{get; private set;}
    private void Awake() {
        playerinputs = new PlayerInputs();
        playerinputs.Movement.Walk.started += OnMovement;
        playerinputs.Movement.Walk.performed += OnMovement;
        playerinputs.Movement.Walk.canceled += OnMovement;

        playerinputs.Actions.Interaction.started += OnInteraction;
        playerinputs.Actions.Interaction.canceled += OnInteraction;

        playerinputs.Actions.Exit.started += OnEscapedPressed;
        playerinputs.Actions.Exit.canceled += OnEscapedPressed;
    }
    void OnMovement(InputAction.CallbackContext context)
    {
        MovementDirection = context.ReadValue<Vector2>();
    }
    void OnInteraction(InputAction.CallbackContext context)
    {
        IsSpaceBarPressed = context.ReadValueAsButton();
    }
    void OnEscapedPressed(InputAction.CallbackContext context)
    {
        IsEscapeKeyPressed = context.ReadValueAsButton();
        Debug.Log("Is Escpae key being pressed: " + IsEscapeKeyPressed);
    }

    private void OnEnable() {
        playerinputs.Enable();
    }
    private void OnDisable() {
        playerinputs.Disable();
    }
}
