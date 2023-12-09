using CameraControl;
using EnemySpawning;
using GameStateMachine;
using GunSystem;
using Player;
using Root;
using UnityEngine;

namespace Level {
    public class LevelController {
        public readonly GunsController GunsController;
        public readonly GunSpawner GunSpawner;
        public readonly EnemySpawner EnemySpawner;
        public readonly PlayerController PlayerController;
        public readonly CameraController CameraController;

        public LevelController(Transform parent, 
            EnemySpawnerConfig spawnerConfig, GunsConfig gunsConfig,
            GunsSpawnerConfig gunsSpawnerConfig, PlayerController playerPrefab, 
            CameraController cameraPrefab) {
            PlayerController = Object.Instantiate(playerPrefab, parent);
            PlayerController.Deactivate();
            
            CameraController = Object.Instantiate(cameraPrefab, parent);
            CameraController.Deactivate();

            GunsController = new GunsController(gunsConfig, PlayerController);
            GunSpawner = new GunSpawner(parent, gunsSpawnerConfig);
            EnemySpawner = new EnemySpawner(spawnerConfig);
        }

        public void Start() {
            GunsController.Init();
            GunSpawner.Start();
            
            EnemySpawner.Start();
           
            PlayerController.Activate();
            CameraController.Activate();

            PlayerController.OnDie += HandlePlayerDeath;
        }

        public void Stop() {
            GunsController.Reset();
            GunSpawner.Stop();

            EnemySpawner.Stop();
            EnemySpawner.Reset();

            PlayerController.Deactivate();
            CameraController.Deactivate();

			PlayerController.OnDie -= HandlePlayerDeath;
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
	}
}
