using Assets.Sources.InputService;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Sources.Gameplay
{
    public class LookRotation : MonoBehaviour
    {
        [SerializeField] private Transform _lookTransform;
        [SerializeField] private Transform _body;
        [SerializeField] private float _horizontalSensivity;
        [SerializeField] private float _verticalSensivity;
        [SerializeField] private float _minVerticalAngle;
        [SerializeField] private float _maxVerticalkAngle;
        [SerializeField] private float _rotationSmoosingSpeed;

        private IInputService _inputService;

        private Vector2 _rotation;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;

            _inputService.Rotated += OnRotated;
        }

        private void OnDestroy() =>
            _inputService.Rotated -= OnRotated;

        private void OnRotated(Vector2 delta)
        {
            _rotation += new Vector2(-delta.y * _verticalSensivity, delta.x * _horizontalSensivity);

            _rotation = new Vector2(Mathf.Clamp(_rotation.x, _minVerticalAngle, _maxVerticalkAngle), _rotation.y);

            _body.rotation = Quaternion.Euler(0, _rotation.y, 0);
            _lookTransform.rotation = Quaternion.Euler(_rotation.x, _rotation.y, 0);
        }
    }
}
