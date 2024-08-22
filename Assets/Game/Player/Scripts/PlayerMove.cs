using Lion.Manager;
using System;
using UnityEngine;

namespace Lion.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMove : MonoBehaviour
    {
        public PlayerManager PlayerManager => PlayerManager.Instance;

        [SerializeField]
        private VirtualJoystick _virtualJoystick;

        private Rigidbody2D _rigidbody2D;

        private Action OnMoveUpdate;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            ChangeMoveMode(MoveMode.Manual);
        }

        private void Update()
        {
            OnMoveUpdate?.Invoke();
        }

        private void ManualMove()
        {
            Vector2 moveDir = new();
            if (_virtualJoystick.IsDragging) moveDir = _virtualJoystick.Vector;

            moveDir += new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            _rigidbody2D.velocity = moveDir.normalized * PlayerManager.Status.MoveSpeed;
        }

        [SerializeField]
        private Transform[] _wayPoints;

        private int _wayPointIndex = 0;

        private Vector3 _startPointForAutoMove;
        private Vector3 _targetPointForAutoMove;

        private void SetMovePosition(Vector3 previous, Vector3 next)
        {
            _startPointForAutoMove = previous;
            _targetPointForAutoMove = next;
        }

        private void AutoMove()
        {
            if (_wayPoints == null || _wayPoints.Length <= 1)
            {
                Debug.LogError("WayPoints is not set.");
                return;
            }

            // 目的地に到着済みの場合Indexを更新する。
            if (MovementUtilities.IsArrived(_startPointForAutoMove, transform.position, _targetPointForAutoMove))
            {
                var previous = _wayPoints[_wayPointIndex].position;

                _wayPointIndex++;
                if (_wayPointIndex >= _wayPoints.Length) _wayPointIndex = 0;

                var next = _wayPoints[_wayPointIndex].position;

                SetMovePosition(previous, next);
            }

            var dir = (_targetPointForAutoMove - transform.position).normalized;
            _rigidbody2D.velocity = dir * PlayerManager.Status.MoveSpeed;
        }

        public void ChangeMoveMode(MoveMode mode)
        {
            if (mode == MoveMode.Manual)
            {
                OnMoveUpdate = ManualMove;
            }
            else if (mode == MoveMode.Auto)
            {
                OnMoveUpdate = AutoMove;
                SetMovePosition(transform.position, _wayPoints[_wayPointIndex].position);
            }
        }
    }

    public enum MoveMode
    {
        Manual,
        Auto
    }
}