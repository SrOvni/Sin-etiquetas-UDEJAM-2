using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    PlayerInputs playerinputs;

    public Vector2 MovementDirection {get;  set;}
    
    public bool Interact { get; private set;}

    public bool PressKeyAction { get; private set; }

    [SerializeField] public UnityEvent OnSpaceBarPressed;
  

    private void Awake() {
        playerinputs = new PlayerInputs();
        playerinputs.Movement.Walk.started += OnMovement;
        playerinputs.Movement.Walk.performed += OnMovement;
        playerinputs.Movement.Walk.canceled += OnMovement;

        playerinputs.Interacts.Interact.started += OnInteract;
        playerinputs.Interacts.Interact.canceled += OnInteract;

        playerinputs.Interacts.WheelChildAction.started += OnPressKey;
    }
    void OnMovement(InputAction.CallbackContext context)
    {
        MovementDirection = context.ReadValue<Vector2>();
    }
    void OnInteract (InputAction.CallbackContext context)
    {
        Interact = context.ReadValueAsButton();
    }

    void OnPressKey(InputAction.CallbackContext context)
    {
        OnSpaceBarPressed?.Invoke();
    }

    private void OnEnable()
    {
        if (playerinputs != null)
        {
            playerinputs.Enable();
        }
        else
        {
            playerinputs = new PlayerInputs();
        }
    }

    private void OnDisable()
    {
        if (playerinputs != null)
        {
            playerinputs.Disable();
        }
    }
}
