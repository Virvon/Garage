using UnityEngine;

namespace Assets.Sources.Infrastructure
{
    public interface IGameFactory
    {
        void CreatePlayer(Vector3 position);
        void CreateUserInterface();
    }
}