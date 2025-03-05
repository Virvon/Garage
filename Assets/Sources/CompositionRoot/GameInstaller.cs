using Assets.Sources.Infrastructure;
using Zenject;

namespace Assets.Sources.Gameplay
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInputService();
            BindGameFactory();
        }

        private void BindGameFactory() =>
            GameFactoryInstaller.Install(Container);

        private void BindInputService() =>
            Container.BindInterfacesTo<InputService.InputService>().AsSingle();
    }
}