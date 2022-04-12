using Code.Infrastructure.Logic.Loot;
using Code.Player;
using JetBrains.Annotations;
using Zenject;

namespace Code.Infrastructure.States
{
  [UsedImplicitly]
  public class GameLoopState : IState
  {
    private readonly IStateMachine _stateMachine;
    private readonly SignalBus _signalBus;

    public GameLoopState(IStateMachine stateMachine, SignalBus signalBus)
    {
      _stateMachine = stateMachine;
      _signalBus = signalBus;
    }

    public void Enter()
    {
      _signalBus.Subscribe<LastLootCollectedSignal>(GameOver);
      _signalBus.Subscribe<PlayerDiedSignal>(GameOver);
    }

    private void GameOver() =>
      _stateMachine.Enter<GameOverState>();

    public void Exit() =>
      _signalBus.Unsubscribe<LastLootCollectedSignal>(GameOver);
  }
}