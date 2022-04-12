using Code.StaticData;

namespace Code.Infrastructure.Services.StaticData
{
  public interface IStaticDataService
  {
    void Load();
    LevelStaticData ForLevel(string levelKey);
    PlayerStaticData ForPlayer();
    LootStaticData ForLoot(LootSpawnId enemySpawnId);
    DroppableStaticData ForDroppable();
    EnemyStaticData ForEnemy(EnemySpawnId enemySpawnId);
  }
}