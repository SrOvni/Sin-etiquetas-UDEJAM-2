using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementPlayer : MonoBehaviour
{
    [Header("Acciones")]
    private InventoryItem _itemInRange = null;
    private NPCMission _nPCMission = null;
    private bool _canInteract = true;

    [Header("Movimiento")]
    public float moveSpeed = 5f;          
    public float smoothTime = 0.1f;
    public bool _canMove = true;

    private InputManager _inputs;         
    private Vector2 currentVelocity;      
    private Rigidbody2D rb;
    private Vector2 targetVelocity;

    [Header("Animaciones")]
    private Animator animator;

    [Header("Referencias")]
    InventorySystem _inventorySystem;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _inputs = GetComponent<InputManager>();
        _inventorySystem = GetComponent<InventorySystem>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        if (_inputs.MovementDirection.magnitude >= 0.1f )
        {
            targetVelocity = _inputs.MovementDirection * moveSpeed;
            rb.velocity = targetVelocity;           
        }
        else
        {
            rb.velocity = Vector2.SmoothDamp(rb.velocity, Vector2.zero, ref currentVelocity, smoothTime);         
        }
        UpdateAnimation();

        if (_itemInRange != null && _inputs.Interact)
        {
            _inventorySystem.AddItem(_itemInRange);
            _itemInRange.OnTake();
        }

        if (_nPCMission != null && _inputs.Interact && _canInteract)
        {
            if (_nPCMission._missionIsStarted)
            {
                _nPCMission.CheckInventoryPlayer();
                _canInteract = false;
            }
        }

        if(_inputs.Interact == false)
        {
            _canInteract = true;
        }
    }

    void UpdateAnimation()
    {                
        animator.SetFloat("Horizontal", _inputs.MovementDirection.x);
        animator.SetFloat("Vertical", _inputs.MovementDirection.y);

        if (_inputs.MovementDirection.x != 0 || _inputs.MovementDirection.y != 0)
        {
            animator.SetFloat("LastDirectionX", _inputs.MovementDirection.x);
            animator.SetFloat("LastDirectionY", _inputs.MovementDirection.y);
        }                   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<InventoryItem>() != null)
        {
            _itemInRange = collision.GetComponent<InventoryItem>();            
        }
        
        if (collision.GetComponent<NPCMission>() != null)
        {
            _nPCMission = collision.GetComponent<NPCMission>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<InventoryItem>() != null)
        {
            _itemInRange = null;
        }
        
        if (collision.GetComponent<NPCMission>() != null)
        {
            _nPCMission = null;
        }
    }
}
