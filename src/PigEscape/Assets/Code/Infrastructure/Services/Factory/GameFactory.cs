using Code.CameraLogic;
using Code.Enemy;
using Code.Infrastructure.Logic;
using Code.Infrastructure.Logic.Droppable;
using Code.Infrastructure.Logic.Enemy;
using Code.Infrastructure.Logic.Loot;
using Code.Infrastructure.Services.StaticData;
using Code.Player;
using Code.StaticData;
using Code.UI.Elements;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Code.Infrastructure.Services.Factory
{
  [UsedImplicitly]
  public class GameFactory : IGameFactory
  {
    private readonly IStaticDataService _staticData;
    private readonly DiContainer _diContainer;

    public GameFactory(DiContainer diContainer, IStaticDataService staticData)
    {
      _staticData = staticData;
      _diContainer = diContainer;
    }

    public GameObject CreatePlayer(Vector3 at)
    {
      PlayerStaticData playerData = _staticData.ForPlayer();
      GameObject player = Object.Instantiate(playerData.Prefab, at, Quaternion.identity);

      IHealth health = player.GetComponent<IHealth>();
      health.Current = playerData.Hp;
      health.Max = playerData.Hp;

      PlayerDrop playerDrop = player.GetComponent<PlayerDrop>();

      _diContainer.Bind<PlayerDrop>().FromInstance(playerDrop);
      _diContainer.Bind<GameObject>().FromInstance(player);
      _diContainer.Bind<IHealth>().FromInstance(health);
      _diContainer.InjectGameObject(player);

      playerDrop.Amount = playerData.BombAmount;

      return player;
    }

    public void CreateVirtualCamera(GameObject followTarget)
    {
      GameObject prefab = Resources.Load<GameObject>(AssetPath.VirtualCameraPath);
      GameObject camera = Object.Instantiate(prefab);

      CameraFollow cameraFollow = camera.GetComponent<CameraFollow>();
      CameraShake cameraShake = camera.GetComponent<CameraShake>();

      _diContainer.Bind<CameraShake>().FromInstance(cameraShake).AsSingle();
      _diContainer.InjectGameObject(camera);

      cameraFollow.Follow(followTarget.transform);
    }

    public void CreateLootSpawner(Vector3 position, LootSpawnId lootSpawnId)
    {
      GameObject prefab = Resources.Load<GameObject>(AssetPath.LootSpawnerPath);
      GameObject spawner = Object.Instantiate(prefab, position, Quaternion.identity);

      _diContainer.InjectGameObject(spawner);

      SpawnPoint spawnPoint = prefab.GetComponent<SpawnPoint>();
      spawnPoint.LootSpawnId = lootSpawnId;
    }

    public void CreateLoot(LootSpawnId lootSpawnId, Transform parent)
    {
      LootStaticData lootData = _staticData.ForLoot(lootSpawnId);
      GameObject loot = Object.Instantiate(lootData.Prefab, parent.position, Quaternion.identity, parent);

      _diContainer.InjectGameObject(loot);
    }

    public void CreateDroppable(Vector3 at)
    {
      DroppableStaticData droppableData = _staticData.ForDroppable();
      GameObject droppable = Object.Instantiate(droppableData.Prefab, at, Quaternion.identity);

      Bomb bomb = droppable.GetComponent<Bomb>();
      bomb.Damage = droppableData.Damage;

      _diContainer.InjectGameObject(droppable);
    }

    public void CreateHud()
    {
      GameObject prefab = Resources.Load<GameObject>(AssetPath.HudPath);
      GameObject hud = Object.Instantiate(prefab);

      PlayerStaticData playerData = _staticData.ForPlayer();
      hud.GetComponent<BombsCounter>().InitText(playerData.BombAmount);

      _diContainer.InjectGameObject(hud);
    }

    public void CreateEnemy(EnemySpawnId enemySpawnId, Transform parent)
    {
      EnemyStaticData enemyData = _staticData.ForEnemy(enemySpawnId);
      GameObject enemy = Object.Instantiate(enemyData.Prefab, parent.position, Quaternion.identity, parent);

      IHealth health = enemy.GetComponent<IHealth>();
      health.Current = enemyData.Hp;
      health.Max = enemyData.Hp;

      Attack attack = enemy.GetComponent<Attack>();
      attack.Damage = enemyData.Damage;
      attack.Cleavage = enemyData.Cleavage;
      attack.AttackCooldown = enemyData.AttackCooldown;

      NavMeshAgent navMeshAgent = enemy.GetComponent<NavMeshAgent>();
      navMeshAgent.speed = enemyData.MoveSpeed;

      _diContainer.InjectGameObject(enemy);

      if (enemySpawnId == EnemySpawnId.Farmer)
        enemy.GetComponent<FarmerAggro>().SwitchFollowOff();
    }

    public void CreateEnemySpawner(Vector3 position, EnemySpawnId enemySpawnId)
    {
      GameObject prefab = Resources.Load<GameObject>(AssetPath.EnemySpawnerPath);
      GameObject spawner = Object.Instantiate(prefab, position, Quaternion.identity);

      _diContainer.InjectGameObject(spawner);

      EnemySpawnPoint spawnPoint = prefab.GetComponent<EnemySpawnPoint>();
      spawnPoint.EnemySpawnId = enemySpawnId;
    }
  }
}