using UnityEngine;
using Zenject;

public class MyInstaller : MonoInstaller
{
    [SerializeField] private UIElementsManager UIElementsManager;
    [SerializeField] private AnimatorManager _animatorManager;
    [SerializeField] private Player _player;
    [SerializeField] private Turner _turner;

    public override void InstallBindings()
    {
        Container.Bind<UIElementsManager>().FromInstance(UIElementsManager);
        Container.Bind<AnimatorManager>().FromInstance(_animatorManager);
        Container.Bind<Player>().FromInstance(_player);
        Container.Bind<Turner>().FromInstance(_turner);
    }
}