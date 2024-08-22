using Lion.Actor;
using Lion.CameraUtility;
using Lion.Player;
using System;
using UnityEngine;

namespace Lion.Ally
{
    public class ReturnState : IState
    {
        private string _runAnimation = "Run";

        private Vector3 _destination;

        public void Enter(AllyController ally)
        {
            ally.Animator.Play(_runAnimation);

            // 目的地を設定する。
            _destination = ActivityArea.Instance.GetRandomPosition();
            // 向きを設定する。
            var direction = _destination - ally.transform.position;
            if (direction.x > 0 && ally.transform.localScale.x < 0)
            {
                ally.transform.localScale = new Vector3(Mathf.Abs(ally.transform.localScale.x), ally.transform.localScale.y, 1);
            }
            else if (direction.x < 0 && ally.transform.localScale.x > 0)
            {
                ally.transform.localScale = new Vector3(-Mathf.Abs(ally.transform.localScale.x), ally.transform.localScale.y, 1);
            }
        }

        private static readonly float BaseMoveSpeed = 2f;
        private float PlayerMoveSpeed => PlayerManager.Instance.LevelManager.CurrentStatus.MoveSpeed;

        public void Update(AllyController ally)
        {
            var allyMoveSpeed = ally.Status.Speed;
            var adjustedMoveSpeed = PlayerMoveSpeed + BaseMoveSpeed + allyMoveSpeed * 0.03f;

            // 目的地に向かって移動する。
            var currentPosition = ally.transform.position;
            var direction = (_destination - currentPosition).normalized;
            ally.Rigidbody2D.velocity = direction * adjustedMoveSpeed;

            // プレイヤーと離れすぎている場合、強制的に目的地に移動させる。
            if (ActivityArea.Instance.IsFarFromArea(ally.transform.position, 5f))
            {
                ally.transform.position = _destination;
            }

            // 目的地に到達したら、IdleStateに遷移する。
            if (Vector2.SqrMagnitude(currentPosition - _destination) < 0.01f)
            {
                ally.SetState<IdleState>();
            }

            // 目的地がプレイヤーから遠すぎる場合、再度目的地を設定する。
            if (ActivityArea.Instance.IsFarFromArea(_destination))
            {
                _destination = ActivityArea.Instance.GetRandomPosition();
            }
        }

        public void Exit(AllyController ally)
        {
            ally.Rigidbody2D.velocity = Vector2.zero;
        }
    }
}