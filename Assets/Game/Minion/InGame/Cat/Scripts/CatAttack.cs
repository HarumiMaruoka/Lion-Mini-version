﻿using Lion.Enemy;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lion.Minion
{
    public class CatAttack : MinionBullet
    {
        [SerializeField] private Animator _animator;

        private void Update()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (EnemyManager.TryGetEnemy(collision.gameObject, out EnemyController enemy))
            {
                enemy.PhysicalDamage(Status.Attack, Controller);
            }
        }
    }
}