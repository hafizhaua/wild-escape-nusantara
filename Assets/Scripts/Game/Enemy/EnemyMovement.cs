using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private float _screenBorder;

    private Rigidbody2D _rigidbody;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _targetDirection;
    private float _changeDirectionCooldown;
    private Camera _camera;
    private Vector2 _originalSpeed;
    private bool _isSlowed;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
        _targetDirection = transform.up;
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity(_isSlowed);
    }

    private void UpdateTargetDirection()
    {
        HandleRandomDirectionChange();
        /*HandlePlayerTargeting();*/
        HandleEnemyOffScreen();
    }

    private void HandleRandomDirectionChange()
    {
        _changeDirectionCooldown -= Time.deltaTime;
        if (_changeDirectionCooldown <= 0)
        {
            float angleChange = Random.Range(-90f, 90f);
            Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.forward);
            _targetDirection = rotation * _targetDirection;

            _changeDirectionCooldown = Random.Range(1f, 5f);
        }
    }

    private void HandlePlayerTargeting()
    {
        if (_playerAwarenessController.AwareOfPlayer)
        {
            _targetDirection = _playerAwarenessController.DirectionToPlayer;
        }
    }

    public void TargetToPlayer()
    {
        _targetDirection = _playerAwarenessController.DirectionToPlayer;
    }

    private void HandleEnemyOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

        if ((screenPosition.x < _screenBorder && _targetDirection.x < 0) || (screenPosition.x > _camera.pixelWidth - _screenBorder && _targetDirection.x > 0))
        {
            _targetDirection = new Vector2(-_targetDirection.x, _targetDirection.y);
        }
        else if ((screenPosition.y < _screenBorder && _targetDirection.y < 0) || (screenPosition.y > _camera.pixelHeight - _screenBorder && _targetDirection.y > 0))
        {
            _targetDirection = new Vector2(_targetDirection.x, -_targetDirection.y);
        }
    }

    private void RotateTowardsTarget()
    {
        if (_targetDirection != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            _rigidbody.SetRotation(rotation);
            // Debug.Log(_targetDirection.magnitude);
        }
        else
        {
            Debug.Log("Enemy is heading to you.");
        }
    }

    private void SetVelocity(bool _isSlowed)
    {
        if (!_isSlowed)
        {
            if (_targetDirection != Vector2.zero)
            {
                _rigidbody.velocity = transform.up * _speed;
            }
        }
        else
        {
            if (_targetDirection != Vector2.zero)
            {
                 _rigidbody.velocity = transform.up * _speed;
                ApplySlow(0.5f);
            }
        }
    }

    public void StopMove()
    {
        _rigidbody.velocity = Vector2.zero;
    }

    public void SetSlowed()
    {
        _isSlowed = true;
        StartCoroutine(ResetSpeed());
    }
    public void ApplySlow(float factor)
    {
        _rigidbody.velocity *= factor;
        Vector2 tempDebug = _rigidbody.velocity;
    }

    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(2);
        _isSlowed = false;
    }
}


