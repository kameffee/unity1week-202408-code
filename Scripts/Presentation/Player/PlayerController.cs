using System;
using UnityEngine;

namespace Unity1week202408.Player
{
    public class PlayerController : MonoBehaviour
    {
        public Vector3 WorldPosition => transform.position;

        [SerializeField]
        private Rigidbody2D _rigidbody2D;

        [SerializeField]
        private PlayerAnimation _playerAnimation;

        [SerializeField]
        private float _moveSpeed = 5f;

        [SerializeField]
        private float _dashSpeed = 10f;

        private Vector2 _moveVector;
        private bool _isMovable;
        private bool _isDash;

        public void SetDash(bool isDash)
        {
            _isDash = isDash;
        }

        private bool IsDash()
        {
            return Input.GetKey(KeyCode.LeftShift)
                   || Input.GetKey(KeyCode.RightShift)
                   || _isDash;
        }

        private void FixedUpdate()
        {
            _rigidbody2D.velocity = _moveVector;
        }

        private void LateUpdate()
        {
            if (_rigidbody2D.velocity.x != 0)
            {
                _playerAnimation.SetDirection(_rigidbody2D.velocity);
            }

            var absoluteX = Mathf.Abs(_rigidbody2D.velocity.x);
            if (absoluteX > 5f)
            {
                _playerAnimation.Run();
            }
            else if (absoluteX > 0.001f)
            {
                _playerAnimation.Walk();
            }
            else
            {
                _playerAnimation.Idle();
            }
        }

        public void Move(Vector2 direction)
        {
            if (!_isMovable)
            {
                _moveVector = new Vector2(0, _rigidbody2D.velocity.y);
                return;
            }

            _moveVector = direction;
            var multiplier = IsDash() ? _dashSpeed : _moveSpeed;
            _moveVector = new Vector2(direction.x * multiplier, _rigidbody2D.velocity.y);
        }

        public void Teleport(Vector3 worldPosition)
        {
            transform.position = new Vector3(worldPosition.x, transform.position.y, transform.position.z);
        }

        public void SetMovable(bool isPlayable)
        {
            _isMovable = isPlayable;
        }
    }
}