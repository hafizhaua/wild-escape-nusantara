using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAwarenessController : MonoBehaviour
{
    public Boolean AwareOfPlayer { get; private set; }

    public Vector2 DirectionToPlayer { get; private set; }

    [SerializeField]
    private float _playerAwarenessDistance;

    private Transform _player;

    public UnityEvent OnAware;

    private void Awake()
    {
       _player = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 enemyToPlayerVector = _player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;

        if (enemyToPlayerVector.magnitude <= _playerAwarenessDistance)
        {
            AwareOfPlayer = true;
            OnAware.Invoke();
        } else AwareOfPlayer = false;
    }
}
