using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RogerHealthController : HealthController
{
    private float _regenerationDelay = 10;
    private float _regenerationRate = 5;
    private float _regenerationTimer;

    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);
        _regenerationTimer = Time.time + _regenerationDelay;
    }
    public void Regenerate()
    {
        _currentHealth += _regenerationRate;
        // _currentHealth = Mathf.Clamp(_currentHealth, 0, _maximumHealth);
        if (_currentHealth > _maximumHealth)
        {
            _currentHealth = _maximumHealth;
        }
        OnHealthChanged.Invoke();
        _regenerationTimer = Time.time + _regenerationDelay;
    }
    public void Update()
    {
        // Debug.Log("Update method is being called!");
        // ... rest of your code
        // Check if enough time has passed to start regeneration
        if (Time.time > _regenerationTimer)
        {
            // Start health regeneration
            Debug.Log("It should regenerate!");
            Regenerate();
        }
        else
        {
            Debug.Log("Not Yet!");
        }

    }
}