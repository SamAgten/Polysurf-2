using UnityEngine;
using Zenject;

public class GameManagerInstaller : MonoInstaller<GameManagerInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<GameManagerStateMainMenu>().FromNew().AsTransient();
        Container.Bind<GameManagerStateFlying>().FromNew().AsTransient();
        Container.Bind<GameManagerStateShowColors>().FromNew().AsTransient();
        Container.Bind<GameManagerStateIntro>().FromNew().AsTransient();
    }
}