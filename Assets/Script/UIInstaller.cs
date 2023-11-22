using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {

        Container.Bind<ITitleController>().To<TitleController>().AsSingle();
        Container.Bind<ITitleModel>().To<TitleModel>().AsSingle();
    }
}