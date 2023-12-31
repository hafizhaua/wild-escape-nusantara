using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class RogerSkill : PlayerMovement
{
    [SerializeField]
    private float _pounceDamage;

    [SerializeField]
    private GameObject _ringPrefab;



    private float _roarRadius;
    private float _distance = 3f;
    private float _duration = 0.5f;
    private bool _isPouncing = false;
    private float _pounceCooldown = 5f;  // Cooldown time in seconds
    private float _roarCooldown = 3f;  // Cooldown time in seconds


    public UnityEvent onSkillUsed;
    public UnityEvent onUpdate;

    public class Cooldowns
    {
        public float pounce;
        public float roar;
    }
    public class RemainingCooldownPercentage
    {
        public float pounce;
        public float roar;
    }
    public Cooldowns remainingCooldown;
    public RemainingCooldownPercentage remainingCooldownPercentage;

    // Start is called before the first frame update
    void Start()
    {
        remainingCooldownPercentage = new RemainingCooldownPercentage();
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
        remainingCooldownPercentage.roar = remainingCooldown.roar / _roarCooldown;
        remainingCooldownPercentage.pounce = remainingCooldown.pounce / _pounceCooldown;
        if (onUpdate != null)
        {
            onUpdate.Invoke();
        }
        else
        {
            Debug.LogError("onUpdate is not assigned!");
        }
    }

    IEnumerator Pounce()
    {
        // Set the flag to trigger when to damage enemy
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
    public string targetTag = "Enemy";
    private void TeritorialRoar()
    {
        Debug.Log("Roar");
        // Get all colliders within a certain radius of the player
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 5);
        // Iterate through the colliders and apply the slow effect to enemies
        // foreach (var collider in colliders)
        // {
        //     var enemyMovement = collider.GetComponent<EnemyMovement>();
        //     if (enemyMovement != null)
        //     {
        //         Debug.Log("Found EnemyMovement on GameObject: " + collider);
        //         enemyMovement.SetSlowed();
        //     }
        //     if (enemyMovement == null)
        //     {
        //         Debug.Log("No EnemyMovement component on GameObject: " + collider);
        //     }
        // }

        foreach (var collider in colliders)
        {
            if (collider.CompareTag(targetTag))
            {
                // The GameObject has the specified tag
                Debug.Log("Found GameObject with tag '" + targetTag + "': " + collider.gameObject.name);
                var enemyMovement = collider.GetComponent<EnemyMovement>();
                if (enemyMovement != null)
                {
                    // Debug.Log("Found EnemyMovement on GameObject: " + collider);
                    enemyMovement.SetSlowed();
                }
                if (enemyMovement == null)
                {
                    Debug.Log("No EnemyMovement component on GameObject: " + collider);
                }
                
            }
        }
        StartCoroutine(SpawnRing());
    }

    IEnumerator SpawnRing()
    {
        GameObject ring = Instantiate(_ringPrefab, transform.position, Quaternion.identity);
        float elapsedTime = 0f;
        float scalingTime = 0.5f;
        while (elapsedTime < scalingTime)
        {
            float scale = Mathf.Lerp(0f, 5f, elapsedTime / scalingTime);
            ring.transform.localScale = new Vector3(scale, scale, scale);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final scale is exactly 5
        ring.transform.localScale = new Vector3(5f, 5f, 1f);
        Destroy(ring);
    }
}
