﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmedMouse : EnemyMouse
{
    private const float bulletSpeed = 3f;
    [SerializeField] internal Transform firePoint;
    [SerializeField] internal GameObject bulletPrefab;
    internal float timeBetweenShots;
    internal float timeToNextShot;

    // Start is called before the first frame update
    void Start()
    {
        base.movementSpeed = 1f;
    }

    // Update is called once per frame
    internal override void FixedUpdate() {
        var playerdirection = transform.position - base.target.position;
        Move(playerdirection.normalized);
        Shoot(playerdirection);
    }

    private void Move(Vector2 direction) {
       base.body.velocity = direction * movementSpeed;
    }

    private void Shoot(Vector2 direction) {
        if (timeToNextShot > 0f) {
            return;
        }

        timeToNextShot = timeBetweenShots;
        var bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        var bullet = bulletObject.GetComponent<Bullet>();
        bullet.direction = transform.position - base.target.position;
        bullet.speed = bulletSpeed;
    }
}
