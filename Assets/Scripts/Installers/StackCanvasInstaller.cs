using Zenject;
using UnityEngine;

public class StackCanvasInstaller : MonoInstaller
{
    [SerializeField] private StackCanvas _data;

    public override void InstallBindings()
    {
        Container.Bind<StackCanvas>().FromInstance(_data).AsSingle().NonLazy();
    }
}
