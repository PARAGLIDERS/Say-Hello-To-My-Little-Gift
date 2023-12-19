using CameraControl;
using EnemySpawning;
using GameStateMachine;
using GunSystem;
using Heals;
using Player;
using Root;
using System.Collections.Generic;
using UnityEngine;

namespace Level {
    public class LevelController {
        public readonly GunsController GunsController;
        public readonly GunSpawner GunSpawner;
        public readonly EnemySpawner EnemySpawner;
        public readonly HealSpawner HealSpawner;
        public readonly PlayerController Player;
        public readonly CameraController Camera;

        private readonly LevelsConfig _config;
        private int _currentLevel;

        public LevelController(Transform parent, GunsControllerConfig gunsConfig,
            GunsSpawnerConfig gunsSpawnerConfig, PlayerController playerPrefab, 
            CameraController cameraPrefab, LevelsConfig config, HealSpawnerConfig healSpawnerConfig) {
            _config = config;

            Player = Object.Instantiate(playerPrefab, parent);
            Player.Deactivate();
            
            Camera = Object.Instantiate(cameraPrefab, parent);
            Camera.Deactivate();

            GunsController = new GunsController(gunsConfig, Player);
            GunSpawner = new GunSpawner(gunsSpawnerConfig);
            
            EnemySpawner = new EnemySpawner();
            HealSpawner = new HealSpawner(healSpawnerConfig);
        }

        public List<LevelsConfigItem> GetLevels() {
            return _config.Items;
        }

        public void ChooseLevel(int level) {
            _currentLevel = level;
        }

        public LevelsConfigItem GetCurrentLevelConfig() {
            return _config.Items[_currentLevel];
        }

        public void Win() {
            Core.DataController.Data.LevelPassed();
            Core.DataController.Save();
            _currentLevel++;
        }        

        public bool HasNextLevel() {
			return _currentLevel < _config.Items.Count;
		}

		public void Start() {
			LevelsConfigItem levelConfig = GetCurrentLevelConfig();

			EnemySpawner.Start(levelConfig.EnemySpawnerConfig);

            GunsController.Init();
            GunSpawner.Start(levelConfig.GunSpawnerGridConfig);

           
            Player.Activate();
            Camera.Activate();

            HealSpawner.Start(levelConfig.HealSpawnerGridConfig);

            Player.OnDie += HandlePlayerDeath;
            EnemySpawner.AllEnemiesKilled += HandleWin;
        }

        public void Stop() {
            GunsController.Reset();
            GunSpawner.Stop();

            EnemySpawner.Stop();
            EnemySpawner.Reset();

            Player.Deactivate();
            Camera.Deactivate();

            HealSpawner.Stop();

			Player.OnDie -= HandlePlayerDeath;
			EnemySpawner.AllEnemiesKilled -= HandleWin;
		}

		public void Pause() {
			GunSpawner.Stop();
			EnemySpawner.Stop();
		}

		public void Update() {
            GunsController.Update();
        }

        public void Dispose() {
            EnemySpawner.Stop();
            EnemySpawner.Dispose();
        }

        private void HandlePlayerDeath() {
            Core.StateController.SetState(StateType.Fail);
		}

        private void HandleWin() {
			Core.StateController.SetState(StateType.Win);
		}
	}
}
