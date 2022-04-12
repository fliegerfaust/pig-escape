using System;
using UnityEngine;

namespace Code.StaticData
{
  [Serializable]
  public class EnemySpawnerData
  {
    public EnemySpawnId EnemySpawnId;
    public Vector3 Position;

    public EnemySpawnerData(EnemySpawnId enemySpawnId, Vector3 position)
    {
      EnemySpawnId = enemySpawnId;
      Position = position;
    }
  }
}