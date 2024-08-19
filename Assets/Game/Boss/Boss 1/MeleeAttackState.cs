using System;
using UnityEngine;

namespace Lion.Enemy.Boss
{
    /// <summary>
    /// �ߋ����U���X�e�[�g�B
    /// </summary>
    public class MeleeAttackState : Stage1BossController.IState
    {
        private readonly string _meleeAttackAnimationName = "Melee Attack";

        public void Enter(Stage1BossController boss)
        {
            boss.Animator.Play(_meleeAttackAnimationName);
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