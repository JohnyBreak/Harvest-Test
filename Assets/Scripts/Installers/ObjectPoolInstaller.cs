using Zenject;
using UnityEngine;

public class ObjectPoolInstaller : MonoInstaller
{
    [SerializeField] private ObjectPool _data;

    public override void InstallBindings()
    {
        Container.Bind<ObjectPool>().FromInstance(_data).AsSingle().NonLazy();
    }
}
