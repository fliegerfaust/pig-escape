using Code.UI;
using Code.UI.Elements;
using JetBrains.Annotations;
using UnityEngine.AI;
using Zenject;

namespace Code.Infrastructure.States
{
  [UsedImplicitly]
  public class LoadMenuState : IState
  {
    private readonly SignalBus _signalBus;
    private readonly IStateMachine _stateMachine;
    private readonly NavMeshSurface2d _navMeshSurface2d;
    private readonly GameOverWindowView _gameOverWindow;
    private readonly MenuWindowView _menuWindow;

    public LoadMenuState(SignalBus signalBus, IStateMachine stateMachine, NavMeshSurface2d navMeshSurface2d,
      GameOverWindowView gameOverWindow, MenuWindowView menuWindow)
    {
      _signalBus = signalBus;
      _stateMachine = stateMachine;
      _navMeshSurface2d = navMeshSurface2d;
      _gameOverWindow = gameOverWindow;
      _menuWindow = menuWindow;
    }

    public void Enter()
    {
      TurnOffGameWorld();
      TurnOffGameOverWindow();
      TurnOnGameMenu();

      _signalBus.Subscribe<StartButtonPressedSignal>(PrepareLevel);
    }

    private void TurnOnGameMenu() =>
      _menuWindow.gameObject.SetActive(true);

    private void TurnOffGameOverWindow() =>
      _gameOverWindow.gameObject.SetActive(false);

    private void TurnOffGameWorld() =>
      _navMeshSurface2d.gameObject.SetActive(false);

    private void PrepareLevel() =>
      _stateMachine.Enter<LoadLevelState>();

    public void Exit()
    {
      _navMeshSurface2d.gameObject.SetActive(true);
      _menuWindow.gameObject.SetActive(false);
      _signalBus.Unsubscribe<StartButtonPressedSignal>(PrepareLevel);
    }
  }
}