using Zenject;
using UnityEngine;

public class SaleManagerInstaller : MonoInstaller
{
    [SerializeField] private SaleManager _data;

    public override void InstallBindings()
    {
        Container.Bind<SaleManager>().FromInstance(_data).AsSingle().NonLazy();
    }
}
