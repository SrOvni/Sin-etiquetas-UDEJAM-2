using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementPlayer : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;          
    public float smoothTime = 0.1f;       

    private InputManager _inputs;         
    private Vector2 currentVelocity;      
    private Rigidbody2D rb;               

    [Header("Animaciones")]
    public Animator animator;             

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _inputs = GetComponent<InputManager>();
    }

    private void Update()
    {
        if (_inputs.MovementDirection.magnitude >= 0.1f )
        {
            Vector2 targetVelocity = _inputs.MovementDirection * moveSpeed;
            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, smoothTime);
            //animator.SetBool("isMoving", true);
            SetAnimationDirection(_inputs.MovementDirection); 
        }
        else
        {
            //animator.SetBool("isMoving", false);
        }
    }

    private void SetAnimationDirection(Vector2 moveDirection)
    {
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

        
        if (angle > -22.5f && angle <= 22.5f) // Derecha
        {
            //animator.SetTrigger("Right");
            Debug.Log("Derecha");
        }
        else if (angle > 22.5f && angle <= 67.5f) 
        {
            //animator.SetTrigger("UpRight");
            Debug.Log("ArribaDerecha");
        }
        else if (angle > 67.5f && angle <= 112.5f) 
        {
            //animator.SetTrigger("Up");
            Debug.Log("Arriba");
        }
        else if (angle > 112.5f && angle <= 157.5f) 
        {
            //animator.SetTrigger("UpLeft");
            Debug.Log("ArribaIzquierda");
        }
        else if (angle > 157.5f || angle <= -157.5f) 
        {
            //animator.SetTrigger("Left");
            Debug.Log("Izquierda");

        }
        else if (angle > -157.5f && angle <= -112.5f) 
        {
            //animator.SetTrigger("DownLeft");
            Debug.Log("AbajoIzquierda");

        }
        else if (angle > -112.5f && angle <= -67.5f) 
        {
            //animator.SetTrigger("Down");
            Debug.Log("Abajo");

        }
        else if (angle > -67.5f && angle <= -22.5f) 
        {
            //animator.SetTrigger("DownRight");
            Debug.Log("AbajoDerecha");

        }
    }
}
