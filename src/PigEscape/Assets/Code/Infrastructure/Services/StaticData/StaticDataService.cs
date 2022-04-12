using System.Collections.Generic;
using System.Linq;
using Code.StaticData;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Infrastructure.Services.StaticData
{
  [UsedImplicitly]
  public class StaticDataService : IStaticDataService
  {
    private const string StaticDataPlayerPath = "StaticData/PlayerData";
    private const string StaticDataLevelPath = "StaticData/LevelData";
    private const string StaticDataLootPath = "StaticData/Loot";
    private const string StaticDataDroppablePath = "StaticData/Droppable/BombData";
    private const string StaticDataEnemyPath = "StaticData/Enemy";

    private PlayerStaticData _player;
    private LevelStaticData _level;
    private Dictionary<LootSpawnId, LootStaticData> _loot;
    private Dictionary<EnemySpawnId, EnemyStaticData> _enemies;
    private DroppableStaticData _droppable;

    public void Load()
    {
      LoadLevel();
      LoadPlayer();
      LoadLootSpawners();
      LoadDroppable();
      LoadEnemies();
    }

    public LevelStaticData ForLevel(string levelKey) =>
      _level;

    public PlayerStaticData ForPlayer() =>
      _player;

    public LootStaticData ForLoot(LootSpawnId typeId) =>
      _loot.TryGetValue(typeId, out LootStaticData staticData) ? staticData : null;

    public DroppableStaticData ForDroppable() =>
      _droppable;

    public EnemyStaticData ForEnemy(EnemySpawnId enemySpawnId) =>
      _enemies.TryGetValue(enemySpawnId, out EnemyStaticData staticData) ? staticData : null;

    private void LoadLevel() =>
      _level = Resources.Load<LevelStaticData>(StaticDataLevelPath);

    private void LoadPlayer() =>
      _player = Resources.Load<PlayerStaticData>(StaticDataPlayerPath);

    private void LoadLootSpawners() =>
      _loot = Resources.LoadAll<LootStaticData>(StaticDataLootPath)
        .ToDictionary(x => x.LootSpawnId, x => x);

    private void LoadDroppable() =>
      _droppable = Resources.Load<DroppableStaticData>(StaticDataDroppablePath);

    private void LoadEnemies()
    {
      _enemies = Resources.LoadAll<EnemyStaticData>(StaticDataEnemyPath)
        .ToDictionary(x => x.EnemySpawnId, x => x);
    }
  }
}