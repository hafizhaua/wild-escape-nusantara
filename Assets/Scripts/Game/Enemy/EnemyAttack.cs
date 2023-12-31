using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float _damageAmount;
    private HealthController healthController;

    private void Start()
    {
        healthController = GetComponent<HealthController>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>())
        {
            var playerHealthController = other.gameObject.GetComponent<HealthController>();
            playerHealthController.TakeDamage(_damageAmount);
        }
    }
}
