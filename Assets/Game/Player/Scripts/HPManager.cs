using Lion.Manager;
using Lion.UI;
using System;
using UnityEngine;

namespace Lion.Player
{
    public class HPManager
    {
        public HPManager() { }

        private float _currentHP;

        public event Action<float> OnHPChanged;

        public float CurrentHP
        {
            get => _currentHP;
            set
            {
                var old = _currentHP;
                _currentHP = Mathf.Clamp(value, 0, MaxHP);
                OnHPChanged?.Invoke(_currentHP);
                if (old != 0 && _currentHP == 0)
                {
                    Die();
                }
            }
        }

        public float MaxHP => PlayerManager.Instance.Status.HP;

        public void Damage(float amount)
        {
            CurrentHP -= amount;
        }

        public void Heal(float amount)
        {
            CurrentHP += amount;
        }

        private void Die()
        {
            GameManager.Instance.GameStateController.CurrentState = GameState.GameOver;
            GameOverWindow.Instance.Show();
        }

        public void Revive()
        {
            CurrentHP = PlayerManager.Instance.Status.HP;
        }
    }
}