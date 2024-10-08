﻿using Lion.Enemy;
using System;
using UnityEngine;

namespace Lion.Weapon.Behaviour.MetallicSwarmModules
{
    public class DroneExplosion : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        public IWeaponParameter Parameter { get; set; }

        private float MagicPower => Parameter == null ? 30f : Parameter.MagicPower;

        private void Update()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (EnemyManager.TryGetEnemy(collision.gameObject, out var enemy))
            {
                enemy.MagicDamage(MagicPower, Parameter?.Actor);
            }
        }
    }
}