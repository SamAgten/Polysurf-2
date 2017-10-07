using UnityEngine;
using Zenject;

public class PolysurfInstaller : MonoInstaller<PolysurfInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<SaveData>().FromNew().AsSingle();
        
        Flash flash = FindObjectOfType<Flash>();
        Container.BindInstance(flash).AsSingle();

        CameraController cameraController = FindObjectOfType<CameraController>();
        Container.BindInstance(cameraController).AsSingle();
    }
}