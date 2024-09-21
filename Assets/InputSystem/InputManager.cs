using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class InputManager : MonoBehaviour
{
    PlayerInputs playerinputs;

    public Vector2 MovementDirection {get; private set;}
    
    public bool Interact { get; private set;}
    private void Awake() {
        playerinputs = new PlayerInputs();
        playerinputs.Movement.Walk.started += OnMovement;
        playerinputs.Movement.Walk.performed += OnMovement;
        playerinputs.Movement.Walk.canceled += OnMovement;

        playerinputs.Interacts.Interact.started += OnInteract;
        playerinputs.Interacts.Interact.canceled += OnInteract;
    }
    void OnMovement(InputAction.CallbackContext context)
    {
        MovementDirection = context.ReadValue<Vector2>();
    }
    void OnInteract (InputAction.CallbackContext context)
    {
        Interact = context.ReadValueAsButton();
    }

    private void OnEnable()
    {
        // Habilita el sistema de entrada
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
        // Deshabilita el sistema de entrada
        if (playerinputs != null)
        {
            playerinputs.Disable();
        }
    }
}
