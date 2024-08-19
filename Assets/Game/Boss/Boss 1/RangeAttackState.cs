using System;
using UnityEngine;

namespace Lion.Enemy.Boss
{
    /// <summary>
    /// �������U���X�e�[�g�B
    /// </summary>
    public class RangeAttackState : Stage1BossController.IState
    {
        private readonly string _rangeAttackAnimationName = "Range Attack";

        public void Enter(Stage1BossController boss)
        {
            boss.Animator.Play(_rangeAttackAnimationName);
        }

        public void Update(Stage1BossController boss)
        {
            boss.Rigidbody2D.velocity = Vector2.zero;

            if (boss.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                // �����Ɏ��̃X�e�[�g�ւ̑J�ڏ����������B
                // �e�X�g
                boss.SetState<IdleState>();
            }
        }

        public void Exit(Stage1BossController boss)
        {

        }
    }
}