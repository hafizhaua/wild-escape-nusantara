using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Vector2 _movementInput;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;
    private Vector2 direction;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _rotationSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        FlipSprite();
        Walk();
    }

    private void Walk()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(
                    _smoothedMovementInput,
                    _movementInput,
                    ref _movementInputSmoothVelocity,
                    0.1f);

        _rigidbody.velocity = _smoothedMovementInput * _speed;
    }


    private void RotateInDirectionOfInput()
    {
        if (_movementInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _smoothedMovementInput);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            _rigidbody.MoveRotation(rotation);
        }
    }

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();
        direction = _movementInput;
    }

    private void FlipSprite()
    {
        bool isPlayerMovementHorizontal = Mathf.Abs(_rigidbody.velocity.x) > Mathf.Epsilon; // Epsilon is way of 0 

        // flip x 
        if (isPlayerMovementHorizontal)
        {
            transform.localScale = new Vector2(-Mathf.Sign(_rigidbody.velocity.x), 1f);
        }

        // is walking
        if (direction.x == 0 && direction.y == 0)
        {
            _animator.SetBool("isWalking", false);
        }
        else
        {
            _animator.SetBool("isWalking", true);
        }

        // flip sprite
        if (_animator.GetBool("isWalking"))
        {
            if (direction.y > 0)
            {
                _animator.SetInteger("direction", 1);
            }
            if (direction.y < 0)
            {
                _animator.SetInteger("direction", -1);
            }
            if (direction.y == 0)
            {
                _animator.SetInteger("direction", 0);
            }
        }


    }


}
