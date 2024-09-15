using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporalMovement : MonoBehaviour
{
    InputManager _inputs;
    Rigidbody2D _rb;
    [SerializeField] float _movementSpeed = 10;
    private void Awake() {
        _inputs = GetComponent<InputManager>();
        _rb = GetComponent<Rigidbody2D>();

    }
    private void FixedUpdate() {
        CharacterMovement();
    }
    void CharacterMovement()
    {
        Vector2 currentPosition = _rb.position;
        Vector2 movement = _inputs.MovementDirection * _movementSpeed;
        Vector2 newPositon = currentPosition + movement * Time.fixedDeltaTime;
        _rb.MovePosition(newPositon);
    }
}
