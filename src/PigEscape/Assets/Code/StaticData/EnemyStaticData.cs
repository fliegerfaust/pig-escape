using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Enemy", order = 0)]
  public class EnemyStaticData : ScriptableObject
  {
    public EnemySpawnId EnemySpawnId;

    [Range(1, 100)] public int Hp;
    [Range(1f, 30f)] public int Damage;
    [Range(0.1f, 5f)] public float Cleavage;
    [Range(0.5f, 10f)] public float MoveSpeed = 2f;
    [Range(0.5f, 3f)] public float AttackCooldown = 1f;
    public GameObject Prefab;
  }
}