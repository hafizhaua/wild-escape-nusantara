using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField]
    private float _damageAmount;

    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private float _bulletSpeed;

    [SerializeField]
    private Transform _gunOffset;

    [SerializeField]
    private float _timeBetweenShots;

    private float _lastFireTime;
    private EnemyMovement _enemyMovement;

    private void Awake()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    /*void Update()
    {
        
    } */

    public void FireBullet()
    {
        float timeSinceLastFire = Time.time - _lastFireTime;

        if (timeSinceLastFire >= _timeBetweenShots)
        {

            GameObject bullet = Instantiate(_bulletPrefab, _gunOffset.position, _gunOffset.rotation);
            Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

            rigidbody.velocity = _bulletSpeed * transform.up;

            _lastFireTime = Time.time;
        }
    }

}
