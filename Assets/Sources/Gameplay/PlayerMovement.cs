using Assets.Sources.InputService;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Sources.Gameplay
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField] private Transform _lookTransform;

        private IInputService _inputService;

        private bool _isMoved;
        private Vector2 _moveDirection;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;

            _isMoved = false;

            _inputService.Moved += OnMoved;
            _inputService.Stopped += OnStopped;
        }

        private void OnDestroy()
        {
            _inputService.Moved -= OnMoved;
            _inputService.Stopped -= OnStopped;
        }

        private void FixedUpdate()
        {
            if(_isMoved)
            {
                Quaternion lookDirection = Quaternion.LookRotation(new Vector3(_lookTransform.forward.x, 0, _lookTransform.forward.z));
                _rigidbody.Move(transform.position + lookDirection * (new Vector3(_moveDirection.x, 0, _moveDirection.y) * _speed * Time.fixedDeltaTime), transform.rotation);
            }
        }

        private void OnMoved(Vector2 direction)
        {
            _moveDirection = direction;
            _isMoved = true;
        }

        private void OnStopped() =>
            _isMoved = false;
    }
}
