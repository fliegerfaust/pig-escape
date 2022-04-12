using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Elements
{
  public class GameOverWindowView : MonoBehaviour
  {
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
      _signalBus = signalBus;
      _restartButton.onClick.AddListener(OnRestartButtonPressed);
      _exitButton.onClick.AddListener(OnExitButtonPressed);
    }

    private void OnRestartButtonPressed() =>
      _signalBus.Fire<RestartButtonPressedSignal>();

    private void OnExitButtonPressed() =>
      Application.Quit();
  }
}