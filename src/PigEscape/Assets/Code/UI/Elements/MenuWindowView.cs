using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Elements
{
  public class MenuWindowView : MonoBehaviour
  {
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;

    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
      _signalBus = signalBus;
      _startButton.onClick.AddListener(OnStartButtonPressed);
      _exitButton.onClick.AddListener(OnExitButtonPressed);
    }

    private void OnStartButtonPressed() =>
      _signalBus.Fire<StartButtonPressedSignal>();

    private void OnExitButtonPressed() =>
      Application.Quit();
  }
}