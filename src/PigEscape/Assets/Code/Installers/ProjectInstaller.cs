using Code.Infrastructure.Logic.Loot;
using Code.Infrastructure.Services.Factory;
using Code.Infrastructure.Services.Input;
using Code.Infrastructure.Services.StaticData;
using Code.Infrastructure.States;
using Code.Player;
using Code.UI;
using Code.UI.Elements;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Code.Installers
{
  public class ProjectInstaller : MonoInstaller
  {
    [SerializeField] private PolygonCollider2D _confiner;
    [SerializeField] private MenuWindowView _menuWindow;
    [SerializeField] private GameOverWindowView _gameOverWindow;
    [SerializeField] private NavMeshSurface2d _navMeshSurface2d;

    public override void InstallBindings()
    {
      DeclareZenjectSignals();

      BindConfiner();
      BindMenuWindows();
      BindSurface();
      BindInputService();
      BindStaticDataService();
      BindLootCounter();
      BindGameFactory();
      BindGameStates();
      BindStateMachine();
    }

    private void BindSurface() =>
      Container.Bind<NavMeshSurface2d>().FromInstance(_navMeshSurface2d);

    private void DeclareZenjectSignals()
    {
      SignalBusInstaller.Install(Container);
      Container.DeclareSignal<StartButtonPressedSignal>();
      Container.DeclareSignal<BombsAmountChangedSignal>();
      Container.DeclareSignal<RestartButtonPressedSignal>();
      Container.DeclareSignal<LastLootCollectedSignal>();
      Container.DeclareSignal<LootCollectedSignal>();
      Container.DeclareSignal<PlayerDiedSignal>();
    }

    private void BindConfiner() =>
      Container.Bind<PolygonCollider2D>().FromInstance(_confiner);

    private void BindMenuWindows()
    {
      Container.Bind<MenuWindowView>().FromInstance(_menuWindow);
      Container.Bind<GameOverWindowView>().FromInstance(_gameOverWindow);
    }

    private void BindInputService()
    {
      if (SystemInfo.deviceType == DeviceType.Handheld)
        Container.BindInterfacesAndSelfTo<MobileInputService>().AsSingle();
      else
        Container.BindInterfacesAndSelfTo<StandaloneInputService>().AsSingle();
    }

    private void BindStaticDataService() =>
      Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();

    private void BindLootCounter() =>
      Container.BindInterfacesAndSelfTo<LootCounter>().AsSingle();

    private void BindGameFactory() =>
      Container.BindInterfacesAndSelfTo<GameFactory>().AsSingle();

    private void BindGameStates()
    {
      Container.BindInterfacesAndSelfTo<LoadMenuState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LoadLevelState>().AsSingle();
      Container.BindInterfacesAndSelfTo<GameLoopState>().AsSingle();
      Container.BindInterfacesAndSelfTo<GameOverState>().AsSingle();
    }

    private void BindStateMachine()
    {
      Container.BindInterfacesAndSelfTo<StateMachine>().AsSingle();
      Container.BindInterfacesAndSelfTo<StateMachineInitializer>().AsSingle();
    }
  }
}