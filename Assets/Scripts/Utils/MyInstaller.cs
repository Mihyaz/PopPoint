using UnityEngine;
using Zenject;
using MihyazUtils.Events;

public class MyInstaller : MonoInstaller
{
    [SerializeField] private UIElementsManager UIElementsManager;
    [SerializeField] private AnimatorManager _animatorManager;
    [SerializeField] private Player _player;
    [SerializeField] private Turner _turner;
    [SerializeField] private GameObject _enemyPool;

    public override void InstallBindings()
    {
        Container.Bind<UIElementsManager>().FromInstance(UIElementsManager);
        Container.Bind<AnimatorManager>().FromInstance(_animatorManager);
        Container.Bind<Player>().FromInstance(_player);
        Container.Bind<Turner>().FromInstance(_turner);

        Container.Bind<EventBase>().AsSingle().OnInstantiated<EventBase>((context, eventBase) => eventBase.Initialize());
        Container
            .BindMemoryPool<Enemy, EnemyPool>()
            .WithInitialSize(3)
            .WithMaxSize(3)
            .FromComponentInNewPrefabResource("Prefabs/Enemy")
            .UnderTransform(_enemyPool.transform);
        Container
            .BindMemoryPool<Collectable, CollectablePool>()
            .WithInitialSize(5)
            .FromComponentInNewPrefabResource("Prefabs/Ruvy")
            .UnderTransform(_turner.transform);
    }
}