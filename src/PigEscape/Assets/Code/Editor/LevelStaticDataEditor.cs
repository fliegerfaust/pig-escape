using System.Linq;
using Code.Infrastructure.Logic.Enemy;
using Code.Infrastructure.Logic.Loot;
using Code.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Editor
{
  [CustomEditor(typeof(LevelStaticData))]
  public class LevelStaticDataEditor : UnityEditor.Editor
  {
    private const string InitialPointTag = "PlayerInitialPosition";

    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      LevelStaticData levelData = (LevelStaticData) target;

      if (GUILayout.Button("Collect"))
      {
        levelData.LootSpawners = FindObjectsOfType<SpawnMarker>()
          .Select(x =>
            new LootSpawnerData(x.LootSpawnId, x.transform.position))
          .ToList();

        levelData.LevelKey = SceneManager.GetActiveScene().name;
        levelData.PlayerInitialPosition = GameObject.FindWithTag(InitialPointTag).transform.position;

        levelData.EnemySpawners = FindObjectsOfType<EnemySpawnMarker>()
          .Select(x =>
            new EnemySpawnerData(x.EnemySpawnId, x.transform.position))
          .ToList();
      }

      EditorUtility.SetDirty(target);
    }
  }
}