using Zenject;
using UnityEngine;
public class GameInstallers : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerCarController>().AsSingle();
        Container.Bind<AICarController>().AsTransient();
    }
}


