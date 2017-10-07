using UnityEngine;
using Zenject;

public class PolysurfInstaller : MonoInstaller<PolysurfInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<SaveData>().FromNew().AsSingle();
    }
}