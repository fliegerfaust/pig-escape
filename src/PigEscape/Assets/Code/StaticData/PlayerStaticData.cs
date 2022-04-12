using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(fileName = "PlayerData", menuName = "StaticData/Player", order = 0)]
  public class PlayerStaticData : ScriptableObject
  {
    [Range(1, 100)] public int Hp;
    [Range(20, 250)] public int Speed;
    public int BombAmount;
    public GameObject Prefab;
  }
}