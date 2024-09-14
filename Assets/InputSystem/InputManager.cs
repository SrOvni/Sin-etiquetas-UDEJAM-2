using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInputs playerinputs = new PlayerInputs();

    public Vector2 MovementDirection {get; private set;}
    private void Awake() {
        playerinputs.Movement.Walk.started += OnMovement;
        playerinputs.Movement.Walk.performed += OnMovement;
        playerinputs.Movement.Walk.canceled += OnMovement;
    }
    void OnMovement(InputAction.CallbackContext context)
    {
        MovementDirection = context.ReadValue<Vector2>();
    }

    private void OnEnable() {
        playerinputs.Enable();
    }
    private void OnDisable() {
        playerinputs.Disable();
    }
}
