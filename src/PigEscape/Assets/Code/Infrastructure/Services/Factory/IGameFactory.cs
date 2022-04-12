using Code.StaticData;
using UnityEngine;

namespace Code.Infrastructure.Services.Factory
{
  public interface IGameFactory
  {
    GameObject CreatePlayer(Vector3 at);
    void CreateVirtualCamera(GameObject followTarget);
    void CreateLootSpawner(Vector3 position, LootSpawnId lootSpawnId);
    void CreateLoot(LootSpawnId lootSpawnId, Transform parent);
    void CreateDroppable(Vector3 at);
    void CreateHud();
    void CreateEnemy(EnemySpawnId enemySpawnId, Transform parent);
    void CreateEnemySpawner(Vector3 position, EnemySpawnId enemySpawnId);
  }
}