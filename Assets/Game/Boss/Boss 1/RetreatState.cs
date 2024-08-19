using Lion.Player;
using System;
using UnityEngine;

namespace Lion.Enemy.Boss
{
    /// <summary>
    /// �v���C���[���痣���X�e�[�g�B
    /// </summary>
    public class RetreatState : Stage1BossController.IState
    {
        private readonly string _runAnimationName = "Run";

        private Vector3 _targetPosition;

        public void Enter(Stage1BossController boss)
        {
            _targetPosition = PlayerController.Instance.transform.position;
            boss.Animator.Play(_runAnimationName);
        }

        public void Update(Stage1BossController boss)
        {
            // �v���C���[���痣���悤�Ɉړ�
            var moveSpeed = boss.Parameters.RetreatState.MoveSpeed;
            boss.Rigidbody2D.velocity = (boss.transform.position - _targetPosition).normalized * moveSpeed;

            // �v���C���[�Ƃ̋������v�Z
            float sqrDistance = Vector2.SqrMagnitude(boss.transform.position - _targetPosition);

            // ��苗���ȏ㗣�ꂽ�牓�����U�����s��
            var arrivalThresholdDistance = boss.Parameters.RetreatState.ArrivalThresholdDistance;
            if (sqrDistance > arrivalThresholdDistance * arrivalThresholdDistance)
            {
                boss.SetState<RangeAttackState>();
            }
        }

        public void Exit(Stage1BossController boss)
        {

        }
    }
}