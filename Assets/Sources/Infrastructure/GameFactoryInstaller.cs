using Assets.Sources.Gameplay;
using UnityEngine;
using Zenject;

namespace Assets.Sources.Infrastructure
{
    public class GameFactoryInstaller : Installer<GameFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameFactory>().AsSingle();

            Container
                .BindFactory<string, UserInterface, UserInterface.Factory>()
                .FromFactory<PrefabGameFactory<UserInterface>>();

            Container
                .BindFactory<string, Vector3, PlayerInventory, PlayerInventory.Factory>()
                .FromFactory<PrefabGameFactory<PlayerInventory>>();
        }
    }
}
