using Assets.Sources.InputService;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Sources.Gameplay
{
    public class PlayerInventory : MonoBehaviour
    {
        private const float RayCastDistance = 2.5f;

        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _itemPoint;
        [SerializeField] private float _pickUpDuration;
        [SerializeField] private float _throwForce;

        private IInputService _inputService;

        private Item _item;

        public event Action Filled;
        public event Action Released;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;

            _inputService.Clicked += OnClicked;
        }

        private void OnDestroy() =>
            _inputService.Clicked -= OnClicked;

        public void Releas()
        {
            if(_item != null)
            {
                _item.Throw(_camera.transform.forward, _throwForce);
                _item = null;
                Released?.Invoke();
            }
        }

        private void OnClicked(Vector2 position)
        {
            Ray ray = _camera.ScreenPointToRay(position);

            if(Physics.Raycast(ray, out RaycastHit hitInfo, RayCastDistance)
                && hitInfo.transform.TryGetComponent(out Item item)
                && item.CanPickUped
                && _item == null)
            {
                _item = item;
                _item.PickUp(_itemPoint, _pickUpDuration, Filled);
            }
        }
        public class Factory : PlaceholderFactory<string, Vector3, PlayerInventory>
        {
        }
    }
}
