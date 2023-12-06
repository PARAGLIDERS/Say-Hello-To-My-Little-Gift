using CameraControl;
using DayNightCycle;
using EnemySpawning;
using GunSystem;
using Player;
using UnityEngine;

namespace Level {
    public class LevelController {
        public readonly GunsController GunsController;
        public readonly GunSpawner GunSpawner;
        public readonly EnemySpawner EnemySpawner;
        public readonly PlayerController PlayerController;
        public readonly CameraController CameraController;
        //public readonly DayNightController DayNightController;

        public LevelController(Transform parent, 
            EnemySpawnerConfig spawnerConfig, GunsConfig gunsConfig,
            GunsSpawnerConfig gunsSpawnerConfig, PlayerController playerPrefab, 
            CameraController cameraPrefab, DayNightConfig dayNightConfig) {
            PlayerController = Object.Instantiate(playerPrefab, parent);
            PlayerController.Deactivate();
            
            CameraController = Object.Instantiate(cameraPrefab, parent);
            CameraController.Deactivate();

            GunsController = new GunsController(gunsConfig, PlayerController);
            GunSpawner = new GunSpawner(parent, gunsSpawnerConfig);
            EnemySpawner = new EnemySpawner(spawnerConfig);

            //DayNightController = new DayNightController(parent, dayNightConfig);
        }

        public void Start() {
            GunsController.Init();
            GunSpawner.Start();
            
            EnemySpawner.Start();
           
            PlayerController.Activate();
            CameraController.Activate();
            //DayNightController.Start();
        }

        public void Stop() {
            GunsController.Reset();
            GunSpawner.Stop();

            EnemySpawner.Stop();
            EnemySpawner.Reset();

            PlayerController.Deactivate();
            CameraController.Deactivate();

            //DayNightController.Stop();
        }

        public void Update() {
            GunsController.Update();
            //DayNightController.Update();
        }

        public void Dispose() {
            EnemySpawner.Stop();
            EnemySpawner.Dispose();
        }
    }
}
