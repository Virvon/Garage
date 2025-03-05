using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Sources.Gameplay
{
    public class UserInterface : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _releasInventoryButtonCanvasGroup;
        [SerializeField] private Button _releasInventoryButton;

        private PlayerInventory _playerInventory;

        [Inject]
        private void Construct(PlayerInventory playerInventory)
        {
            _playerInventory = playerInventory;

            _playerInventory.Filled += OnInventoryFilled;
            _playerInventory.Released += OnInventoryReleased;
            _releasInventoryButton.onClick.AddListener(OnReleasButtonClicked);
        }

        private void OnDestroy()
        {
            _playerInventory.Filled -= OnInventoryFilled;
            _playerInventory.Released -= OnInventoryReleased;
            _releasInventoryButton.onClick.RemoveListener(OnReleasButtonClicked);
        }

        private void OnInventoryFilled() =>
            SetReleaseInventoryButtonActive(true);

        private void OnInventoryReleased() =>
            SetReleaseInventoryButtonActive(false);

        private void OnReleasButtonClicked() =>
            _playerInventory.Releas();

        private void SetReleaseInventoryButtonActive(bool isActive)
        {
            _releasInventoryButtonCanvasGroup.alpha = isActive ? 1 : 0;
            _releasInventoryButtonCanvasGroup.blocksRaycasts = isActive;
            _releasInventoryButtonCanvasGroup.interactable = isActive;
        }

        public class Factory : PlaceholderFactory<string, UserInterface>
        {
        }
    }

}
