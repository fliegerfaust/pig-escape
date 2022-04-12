using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(fileName = "DroppableData", menuName = "StaticData/Droppable", order = 0)]
  public class DroppableStaticData : ScriptableObject
  {
    [Range(0, 2)] public float Radius;
    [Range(1, 100)] public int Damage;
    public GameObject Prefab;
  }
}