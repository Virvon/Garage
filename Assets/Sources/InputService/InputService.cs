using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Sources.InputService
{
    public class InputService : IDisposable, IInputService
    {
        private readonly InputActionsSheme _inputActionsSheme;

        private Vector2 _clickPosition;
        private bool _isRotated;
        private bool _isMoved;

        public InputService()
        {
            _inputActionsSheme = new();
            _inputActionsSheme.Enable();

            _inputActionsSheme.PlayerInput.Movement.performed += OnMoved;
            _inputActionsSheme.PlayerInput.Movement.canceled += OnStopped;
            _inputActionsSheme.PlayerInput.Rotation.performed += OnRotated;
            _inputActionsSheme.PlayerInput.Click.started += OnClickStarted;
            _inputActionsSheme.PlayerInput.Click.canceled += OnClickFinished;
        }

        public event Action<Vector2> Moved;
        public event Action Stopped;
        public event Action<Vector2> Rotated;
        public event Action<Vector2> Clicked;

        public void Dispose()
        {
            _inputActionsSheme.PlayerInput.Movement.performed -= OnMoved;
            _inputActionsSheme.PlayerInput.Movement.canceled -= OnStopped;
            _inputActionsSheme.PlayerInput.Rotation.performed -= OnRotated;
            _inputActionsSheme.PlayerInput.Click.started -= OnClickStarted;
            _inputActionsSheme.PlayerInput.Click.canceled -= OnClickFinished;
        }

        private void OnMoved(InputAction.CallbackContext callbackContext)
        {
            _isMoved = true;
            Moved?.Invoke(callbackContext.ReadValue<Vector2>());
        }

        private void OnStopped(InputAction.CallbackContext callbackContext)
        {
            _isMoved = false;
            Stopped?.Invoke();
        }

        private void OnRotated(InputAction.CallbackContext callbackContext)
        {
            if (_isMoved)
                return;

            Vector2 delta = callbackContext.ReadValue<Vector2>();

            if (delta.magnitude > 1f)
            {
                _isRotated = true;
                Rotated?.Invoke(delta);
            }
        }        

        private void OnClickStarted(InputAction.CallbackContext callbackContext)
        {
            _isRotated = false;
            _clickPosition = callbackContext.ReadValue<Vector2>();
        }

        private void OnClickFinished(InputAction.CallbackContext callbackContext)
        {
            if (_isRotated == false)
                Clicked?.Invoke(_clickPosition);
        }
    }
}
