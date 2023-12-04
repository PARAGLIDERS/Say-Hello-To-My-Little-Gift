using CameraControl;
using DayNightCycle;
using EnemySpawning;
using GunSystem;
using Player;
using UnityEngine;

namespace Level {
    public class LevelController {
        public readonly GunsController GunsController;
        public readonly EnemySpawner EnemySpawner;
        public readonly PlayerController PlayerController;
        public readonly CameraController CameraController;
        //public readonly DayNightController DayNightController;

        public LevelController(Transform parent, 
            EnemySpawnerConfig spawnerConfig, GunsConfig gunsConfig, 
            PlayerController playerPrefab, CameraController cameraPrefab,
            DayNightConfig dayNightConfig) {
            PlayerController = Object.Instantiate(playerPrefab, parent);
            PlayerController.Deactivate();
            
            CameraController = Object.Instantiate(cameraPrefab, parent);
            CameraController.Deactivate();

            GunsController = new GunsController(gunsConfig, PlayerController);
            EnemySpawner = new EnemySpawner(spawnerConfig);

            //DayNightController = new DayNightController(parent, dayNightConfig);
        }

        public void Start() {
            GunsController.Init();
            EnemySpawner.Start();
            PlayerController.Activate();
            CameraController.Activate();
            //DayNightController.Start();
        }

        public void Stop() {
            EnemySpawner.Stop();
            PlayerController.Deactivate();
            CameraController.Deactivate();
            GunsController.Reset();
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
