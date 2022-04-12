using JetBrains.Annotations;
using Zenject;

namespace Code.Infrastructure.States
{
  [UsedImplicitly]
  public class StateMachineInitializer : IInitializable
  {
    private readonly IStateMachine _stateMachine;
    private readonly LoadLevelState _loadLevelState;
    private readonly GameLoopState _gameLoopState;
    private readonly LoadMenuState _loadMenuState;
    private readonly GameOverState _gameOverState;

    public StateMachineInitializer(IStateMachine stateMachine, LoadLevelState loadLevelState,
      GameLoopState gameLoopState, LoadMenuState loadMenuState, GameOverState gameOverState)
    {
      _stateMachine = stateMachine;
      _loadLevelState = loadLevelState;
      _gameLoopState = gameLoopState;
      _loadMenuState = loadMenuState;
      _gameOverState = gameOverState;
    }

    public void Initialize()
    {
      RegisterStates();
      EnterLoadLevelState();
    }

    private void RegisterStates()
    {
      _stateMachine.RegisterState(_loadMenuState);
      _stateMachine.RegisterState(_loadLevelState);
      _stateMachine.RegisterState(_gameLoopState);
      _stateMachine.RegisterState(_gameOverState);
    }

    private void EnterLoadLevelState() =>
      _stateMachine.Enter<LoadMenuState>();
  }
}