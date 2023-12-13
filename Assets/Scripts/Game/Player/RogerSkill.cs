using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class RogerSkill : PlayerMovement
{
    [SerializeField]
    private float _pounceDamage;
    private float _distance = 3f;
    private float _duration = 0.5f;
    private bool _isPouncing = false;
    private float _pounceCooldown = 5f;  // Cooldown time in seconds
    private float _roarCooldown = 3f;  // Cooldown time in seconds

    private float _roarRadius = 5f;
    public UnityEvent onSkillUsed;

    public class Cooldowns
    {
        public float pounce;
        public float roar;
    }
    public Cooldowns remainingCooldown;
    // Start is called before the first frame update
    void Start()
    {
        remainingCooldown = new Cooldowns();
        remainingCooldown.pounce = _pounceCooldown;
        remainingCooldown.roar = _roarCooldown;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_isPouncing)
        {
            if (other.gameObject.GetComponent<EnemyMovement>())
            {
                var enemyHealthController = other.gameObject.GetComponent<HealthController>();
                enemyHealthController.TakeDamage(_pounceDamage);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && remainingCooldown.pounce <= 0)
        {
            _animator.SetTrigger("attack");
            onSkillUsed.Invoke();
            StartCoroutine(Pounce());
            remainingCooldown.pounce = _pounceCooldown;  // Start cooldown
        }
        if (Input.GetKeyDown(KeyCode.V) && remainingCooldown.roar <= 0)
        {
            _animator.SetTrigger("attack");
            onSkillUsed.Invoke();
            TeritorialRoar();
            remainingCooldown.roar = _roarCooldown;  // Start cooldown
        }
        if (remainingCooldown.pounce > 0)
        {
            remainingCooldown.pounce -= Time.deltaTime;
        }
        if (remainingCooldown.roar > 0)
        {
            remainingCooldown.roar -= Time.deltaTime;
        }
    }

    IEnumerator Pounce()
    {
        // Set the flag to prevent multiple dashes at the same time
        _isPouncing = true;

        // Store the initial position of the player
        Vector2 startPosition = transform.position;

        // Calculate the target position based on the dash distance
        Vector2 targetPosition;
        Vector2 swappedDirection = new Vector2(_direction.y, _direction.x);

        if (_movementInput.magnitude > 0.0001f)
        {
            targetPosition = startPosition + _movementInput * _distance;
        }
        else
        {
            targetPosition = startPosition + _direction * _distance;
        }

        // Record the start time
        float startTime = Time.time;

        // Perform the dash over a specific duration
        while (Time.time - startTime < _duration)
        {
            // Interpolate the position based on time
            transform.position = Vector3.Lerp(startPosition, targetPosition, (Time.time - startTime) / _duration);

            // Yielding null allows the loop to wait until the next frame
            yield return null;
        }

        // Ensure that the final position is exactly the target position
        transform.position = targetPosition;
        // Reset the flag after the dash is complete
        _isPouncing = false;
    }

    private void TeritorialRoar()
    {
        Debug.Log("Roar");
        // Get all colliders within a certain radius of the player
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _roarRadius);
        // Iterate through the colliders and apply the slow effect to enemies
        foreach (var collider in colliders)
        {
            var enemyMovement = collider.gameObject.GetComponent<EnemyMovement>();
            if (enemyMovement != null)
            {
                enemyMovement.SetSlowed();
            }
        }
    }
}
