using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(fileName = "LootData", menuName = "StaticData/Loot", order = 0)]
  public class LootStaticData : ScriptableObject
  {
    public LootSpawnId LootSpawnId;
    public GameObject Prefab;
  }
}