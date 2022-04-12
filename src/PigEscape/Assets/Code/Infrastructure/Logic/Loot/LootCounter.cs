using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Logic.Loot
{
  [UsedImplicitly]
  public class LootCounter : IInitializable
  {
    private readonly SignalBus _signalBus;
    public int Counter { get; set; }
    public int MaxCounter { get; set; }

    public LootCounter(SignalBus signalBus) =>
      _signalBus = signalBus;

    public void Initialize() =>
      _signalBus.Subscribe<LootCollectedSignal>(Increment);

    private void Increment()
    {
      Counter += 1;

      if (Counter >= MaxCounter)
      {
        _signalBus.Fire<LastLootCollectedSignal>();
        Debug.Log("Last collected!");
        _signalBus.Unsubscribe<LootCollectedSignal>(Increment);
      }
    }
  }
}