using Code.Infrastructure.Logic.Loot;
using Code.Infrastructure.Services.Factory;
using Code.Infrastructure.Services.StaticData;
using Code.StaticData;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Infrastructure.States
{
  [UsedImplicitly]
  public class LoadLevelState : IState
  {
    private readonly IStateMachine _stateMachine;
    private readonly IGameFactory _gameFactory;
    private readonly IStaticDataService _staticData;
    private readonly LootCounter _lootCounter;

    public LoadLevelState(IStateMachine stateMachine, IGameFactory gameFactory, IStaticDataService staticData,
      SignalBus signalBus, LootCounter lootCounter)
    {
      _stateMachine = stateMachine;
      _gameFactory = gameFactory;
      _staticData = staticData;
      _lootCounter = lootCounter;
    }

    public void Enter()
    {
      LoadData();
      InitGameWorld();
    }

    private void LoadData() =>
      _staticData.Load();

    private void InitGameWorld()
    {
      LevelStaticData levelData = LevelStaticData();
      GameObject player = InitPlayer(levelData);

      InitLootCounter(levelData);
      InitCamera(player);
      InitLootSpawners(levelData);
      InitHud();
      InitEnemySpawners(levelData);

      StartGame();
    }

    private void InitLootCounter(LevelStaticData levelData)
    {
      _lootCounter.MaxCounter = levelData.LootSpawners.Count;
      _lootCounter.Counter = 0;
    }

    private void StartGame() =>
      _stateMachine.Enter<GameLoopState>();

    private GameObject InitPlayer(LevelStaticData levelData) =>
      _gameFactory.CreatePlayer(levelData.PlayerInitialPosition);

    private void InitCamera(GameObject player) =>
      _gameFactory.CreateVirtualCamera(player);


    private void InitLootSpawners(LevelStaticData levelData)
    {
      foreach (LootSpawnerData spawnerData in levelData.LootSpawners)
        _gameFactory.CreateLootSpawner(spawnerData.Position, spawnerData.LootSpawnId);
    }

    private void InitHud() =>
      _gameFactory.CreateHud();

    private void InitEnemySpawners(LevelStaticData levelData)
    {
      foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
        _gameFactory.CreateEnemySpawner(spawnerData.Position, spawnerData.EnemySpawnId);
    }

    public void Exit()
    {
    }

    private LevelStaticData LevelStaticData() =>
      _staticData.ForLevel(SceneManager.GetActiveScene().name);
  }
}