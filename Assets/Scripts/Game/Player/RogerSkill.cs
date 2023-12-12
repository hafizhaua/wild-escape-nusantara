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
    private bool isDashing = false;
    public UnityEvent onSkillUsed;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isDashing)
        {
            if (other.gameObject.GetComponent<EnemyMovement>())
            {
                var enemyHealthController = other.gameObject.GetComponent<HealthController>();
                enemyHealthController.TakeDamage(_pounceDamage);
                // Debug.Log("Tabrakan");
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("attack");
            onSkillUsed.Invoke();
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        // Set the flag to prevent multiple dashes at the same time
        isDashing = true;

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
        // Debug.Log("Direction: " + _direction);
        // Debug.Log("Swapped Direction: " + swappedDirection);
        // Debug.Log("Movement Input: " + _movementInput);
        // Debug.Log("Dash Distance: " + _distance);
        // Debug.Log("Start Position: " + startPosition);
        // Debug.Log("Target Position: " + targetPosition);
        // Debug.Log("Calculated Distance: " + Vector3.Distance(startPosition, targetPosition));


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
        isDashing = false;
    }
}
