using Assets.Sources.Infrastructure;
using UnityEngine;
using Zenject;

namespace Assets.Sources.Gameplay
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private Transform _playerPoint;

        private IGameFactory _gameFactory;

        [Inject]
        private void Construct(IGameFactory gameFactory) =>
            _gameFactory = gameFactory;

        private void Start()
        {
            _gameFactory.CreatePlayer(_playerPoint.position);
            _gameFactory.CreateUserInterface();
        }
    }
}