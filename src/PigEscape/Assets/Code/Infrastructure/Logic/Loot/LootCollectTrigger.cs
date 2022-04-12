using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Logic.Loot
{
  public class LootCollectTrigger : MonoBehaviour
  {
    private const string Player = "Player";

    private SignalBus _signalBus;
    private bool _triggered;

    [Inject]
    public void Construct(SignalBus signalBus) =>
      _signalBus = signalBus;

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (_triggered)
        return;

      if (other.CompareTag(Player))
      {
        _signalBus.Fire<LootCollectedSignal>();
        DestroyLoot();
        _triggered = true;
      }
    }

    private void DestroyLoot() =>
      Destroy(gameObject);
  }
}