using CameraControl;
using EnemySpawning;
using GameStateMachine;
using GunSystem;
using Music;
using Player;
using Root;
using System.Collections.Generic;
using UnityEngine;

namespace Level {
    public class LevelController {

        public readonly GunsController GunsController;
        public readonly GunSpawner GunSpawner;
        public readonly EnemySpawner EnemySpawner;
        public readonly PlayerController PlayerController;
        public readonly CameraController CameraController;

        private readonly LevelsConfig _config;
        private int _currentLevel;

        public LevelController(Transform parent, GunsConfig gunsConfig,
            GunsSpawnerConfig gunsSpawnerConfig, PlayerController playerPrefab, 
            CameraController cameraPrefab, LevelsConfig config) {
            _config = config;

            PlayerController = Object.Instantiate(playerPrefab, parent);
            PlayerController.Deactivate();
            
            CameraController = Object.Instantiate(cameraPrefab, parent);
            CameraController.Deactivate();

            GunsController = new GunsController(gunsConfig, PlayerController);
            GunSpawner = new GunSpawner(parent, gunsSpawnerConfig);
            EnemySpawner = new EnemySpawner();
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
        }

        public void Start() {
            GunsController.Init();
            GunSpawner.Start();

            EnemySpawner.Start(GetCurrentLevelConfig().EnemySpawnerConfig);
           
            PlayerController.Activate();
            CameraController.Activate();

            PlayerController.OnDie += HandlePlayerDeath;
            EnemySpawner.AllEnemiesKilled += HandleWin;
        }

        public void Stop() {
            GunsController.Reset();
            GunSpawner.Stop();

            EnemySpawner.Stop();
            EnemySpawner.Reset();

            PlayerController.Deactivate();
            CameraController.Deactivate();

			PlayerController.OnDie -= HandlePlayerDeath;
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
