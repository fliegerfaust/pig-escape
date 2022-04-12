using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level", order = 0)]
  public class LevelStaticData : ScriptableObject
  {
    public string LevelKey;
    public Vector3 PlayerInitialPosition;
    public List<LootSpawnerData> LootSpawners;
    public List<EnemySpawnerData> EnemySpawners;
  }
}