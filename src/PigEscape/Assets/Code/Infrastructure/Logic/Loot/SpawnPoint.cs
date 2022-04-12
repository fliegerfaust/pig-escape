using Code.Infrastructure.Services.Factory;
using Code.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Logic.Loot
{
  public class SpawnPoint : MonoBehaviour
  {
    public LootSpawnId LootSpawnId;

    private IGameFactory _gameFactory;

    [Inject]
    public void Construct(IGameFactory gameFactory) =>
      _gameFactory = gameFactory;

    private void Start() =>
      Spawn();

    private void Spawn() =>
      _gameFactory.CreateLoot(LootSpawnId, transform);
  }
}