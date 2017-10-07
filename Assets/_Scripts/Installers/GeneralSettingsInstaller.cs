using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GeneralSettingsInstaller", menuName = "Installers/GeneralSettingsInstaller")]
public class GeneralSettingsInstaller : ScriptableObjectInstaller<GeneralSettingsInstaller>
{
    public SaveData.DefaultData defaultSaveData;

    public override void InstallBindings()
    {
        Container.BindInstance(defaultSaveData).AsSingle();
    }
}