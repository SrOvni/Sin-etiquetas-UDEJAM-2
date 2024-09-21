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
    public Animator animator;

    [Header("Referencias")]
    InventorySystem _inventorySystem;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _inputs = GetComponent<InputManager>();
        _inventorySystem = GetComponent<InventorySystem>();
    }

    private void Update()
    {
        if (_inputs.MovementDirection.magnitude >= 0.1f )
        {
            targetVelocity = _inputs.MovementDirection * moveSpeed;
            rb.velocity = targetVelocity;
            //animator.SetBool("isMoving", true);
            SetAnimationDirection(_inputs.MovementDirection); 
        }
        else
        {
            rb.velocity = Vector2.SmoothDamp(rb.velocity, Vector2.zero, ref currentVelocity, smoothTime);

            if(rb.velocity == Vector2.zero)
            {
                //animator.SetBool("isMoving", false);
                //Debug.Log("Idle");
            }
        }


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

    private void SetAnimationDirection(Vector2 moveDirection)
    {
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

        
        if (angle > -22.5f && angle <= 22.5f) // Derecha
        {
            //animator.SetTrigger("Right");
            //Debug.Log("Derecha");
        }
        else if (angle > 22.5f && angle <= 67.5f) 
        {
            //animator.SetTrigger("UpRight");
            //Debug.Log("ArribaDerecha");
        }
        else if (angle > 67.5f && angle <= 112.5f) 
        {
            //animator.SetTrigger("Up");
            //Debug.Log("Arriba");
        }
        else if (angle > 112.5f && angle <= 157.5f) 
        {
            //animator.SetTrigger("UpLeft");
            //Debug.Log("ArribaIzquierda");
        }
        else if (angle > 157.5f || angle <= -157.5f) 
        {
            //animator.SetTrigger("Left");
            //Debug.Log("Izquierda");

        }
        else if (angle > -157.5f && angle <= -112.5f) 
        {
            //animator.SetTrigger("DownLeft");
            //Debug.Log("AbajoIzquierda");

        }
        else if (angle > -112.5f && angle <= -67.5f) 
        {
            //animator.SetTrigger("Down");
            //Debug.Log("Abajo");

        }
        else if (angle > -67.5f && angle <= -22.5f) 
        {
            //animator.SetTrigger("DownRight");
            //Debug.Log("AbajoDerecha");

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
