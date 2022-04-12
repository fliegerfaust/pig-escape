using Code.UI;
using Code.UI.Elements;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Infrastructure.States
{
  [UsedImplicitly]
  public class GameOverState : IState
  {
    private readonly GameOverWindowView _gameOverWindow;
    private readonly SignalBus _signalBus;

    public GameOverState(GameOverWindowView gameOverWindow, SignalBus signalBus)
    {
      _signalBus = signalBus;
      _gameOverWindow = gameOverWindow;
    }

    public void Enter()
    {
      _gameOverWindow.gameObject.SetActive(true);
      _signalBus.Subscribe<RestartButtonPressedSignal>(RestartGame);
    }

    private void RestartGame() =>
      SceneManager.LoadScene(0);

    public void Exit()
    {
    }
  }
}