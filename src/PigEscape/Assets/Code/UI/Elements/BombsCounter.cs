using Code.Player;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.UI.Elements
{
  public class BombsCounter : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _text;
    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
      _signalBus = signalBus;
      _signalBus.Subscribe<BombsAmountChangedSignal>(UpdateText);
    }

    public void InitText(int value) =>
      _text.SetText($"Bombs: {value}");

    private void UpdateText(BombsAmountChangedSignal signal) =>
      _text.SetText($"Bombs: {signal.Value}");

    private void OnDestroy() =>
      _signalBus.Unsubscribe<BombsAmountChangedSignal>(UpdateText);
  }
}