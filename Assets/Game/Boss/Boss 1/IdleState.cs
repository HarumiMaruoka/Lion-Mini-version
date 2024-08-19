using System;
using UnityEngine;

namespace Lion.Enemy.Boss
{
    /// <summary>
    /// �ҋ@�X�e�[�g�B
    /// </summary>
    public class IdleState : Stage1BossController.IState
    {
        private string _idleAnimationName = "Idle";

        // �����_�����ԑҋ@���āA���̍s�������肷��B
        private float _waitMinTime;
        private float _waitMaxTime;

        private float _idleTime;

        public void Enter(Stage1BossController boss)
        {
            boss.Animator.Play(_idleAnimationName);
            _idleTime = UnityEngine.Random.Range(_waitMinTime, _waitMaxTime);
        }

        public void Update(Stage1BossController boss)
        {
            boss.Rigidbody2D.velocity = Vector2.zero;

            _idleTime -= Time.deltaTime;

            if (_idleTime < 0)
            {
                // �����Ɏ��̃X�e�[�g�ւ̑J�ڏ����������B
                // �e�X�g�p�Ƀ����_���Ŏ��̃X�e�[�g�����肷��B
                var rand = UnityEngine.Random.Range(0, 3);
                if (rand == 0)
                {
                    boss.SetState<ApproachingState>();
                }
                else if (rand == 1)
                {
                    boss.SetState<RetreatState>();
                }
                else
                {
                    boss.SetState<ObservingState>();
                }
            }
        }

        public void Exit(Stage1BossController boss)
        {

        }
    }
}