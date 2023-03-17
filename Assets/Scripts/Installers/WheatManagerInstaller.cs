using Zenject;
using UnityEngine;

public class WheatManagerInstaller : MonoInstaller
{
    [SerializeField] private WheatManager _data;

    public override void InstallBindings()
    {
        Container.Bind<WheatManager>().FromInstance(_data).AsSingle().NonLazy();
    }
}
