using Assets.Sources.Gameplay;
using UnityEngine;
using Zenject;

namespace Assets.Sources.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private const string PlayerPath = "Player";
        private const string UserInterfacePath = "Canvas";

        private readonly DiContainer _container;
        private readonly PlayerInventory.Factory _playerFactory;
        private readonly UserInterface.Factory _userInterfaceFactory;

        public GameFactory(DiContainer container, PlayerInventory.Factory playerFactory, UserInterface.Factory userInterfaceFactory)
        {
            _container = container;
            _playerFactory = playerFactory;
            _userInterfaceFactory = userInterfaceFactory;
        }

        public void CreatePlayer(Vector3 position)
        {
            PlayerInventory playerInventory = _playerFactory.Create(PlayerPath, position);
            _container.BindInstance(playerInventory).AsSingle();
        }

        public void CreateUserInterface() =>
            _userInterfaceFactory.Create(UserInterfacePath);
    }
}
