using System;
using UnityEngine;

namespace Code.StaticData
{
  [Serializable]
  public class LootSpawnerData
  {
    public LootSpawnId LootSpawnId;
    public Vector3 Position;

    public LootSpawnerData(LootSpawnId lootSpawnId, Vector3 position)
    {
      LootSpawnId = lootSpawnId;
      Position = position;
    }
  }
}