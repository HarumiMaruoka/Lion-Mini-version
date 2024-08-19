using Lion.Player;
using System;
using UnityEngine;

namespace Lion.Enemy.Boss
{
    /// <summary>
    /// �v���C���[�ɋ߂Â��X�e�[�g�B
    /// </summary>
    public class ApproachingState : Stage1BossController.IState
    {
        private string _runAnimationName = "Run";

        private Vector3 _targetPosition;

        public void Enter(Stage1BossController boss)
        {
            _targetPosition = PlayerController.Instance.transform.position;
            boss.Animator.Play(_runAnimationName);
        }

        public void Update(Stage1BossController boss)
        {
            // �v���C���[�Ɍ������Ĉړ�
            var moveSpeed = boss.Parameters.ApproachingState.MoveSpeed;
            boss.Rigidbody2D.velocity = (_targetPosition - boss.transform.position).normalized * moveSpeed;

            // �v���C���[�Ƃ̋������v�Z
            float sqrDistance = Vector2.SqrMagnitude(boss.transform.position - _targetPosition);

            // ��苗���ȓ��ɋ߂Â�����ߋ����U�����s��
            var arrivalThresholdDistance = boss.Parameters.ApproachingState.ArrivalThresholdDistance;
            if (sqrDistance < arrivalThresholdDistance * arrivalThresholdDistance)
            {
                boss.SetState<MeleeAttackState>();
            }
        }

        public void Exit(Stage1BossController boss)
        {

        }
    }
}