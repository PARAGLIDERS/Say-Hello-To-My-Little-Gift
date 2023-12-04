using EnemySpawning;
using GunSystem;
using GameStateMachine;
using PoolSystem;
using SfxSystem;
using Ui;
using UnityEngine;
using Player;
using Level;
using Unity.VisualScripting;
using DayNightCycle;

namespace Root {
	public static class Core {
		public static CoroutineRunner CoroutineRunner { get; private set; }
		public static UiController UiController { get; private set; }
		public static PoolController PoolController { get; private set; }
        public static SfxController SfxController { get; private set; }
        public static LevelController LevelController { get; private set; }
        public static InputController InputController { get; private set; }
		public static StateController StateController { get; private set; }

		public static void Init(Transform parent, Resources resources) {
            parent.AddComponent<AudioListener>();
            Object.Instantiate(resources.VolumePrefab, parent);

            CoroutineRunner = new GameObject("coroutine runner").AddComponent<CoroutineRunner>();
			CoroutineRunner.transform.SetParent(parent);
			
			UiController = new UiController(parent, resources.CanvasPrefab, resources.ScreenConfig);
            PoolController = new PoolController(parent, resources.PoolConfig);
            SfxController = new SfxController(parent, resources.SfxConfig);
            LevelController = new LevelController(parent, 
                resources.EnemySpawnerConfig, resources.GunsConfig,
                resources.PlayerPrefab, resources.CameraPrefab,
                resources.DayNightConfig);

            InputController = new InputController();
            StateController = new StateController();

			StateController.SetState(StateType.Boot);
		}

		public static void Update() {
			StateController?.Update();
		}

        // i dont think it's nessesary 
        // but why not :)
		public static void Dispose() {
			PoolController.Dispose();
			CoroutineRunner.StopAll();
			Object.Destroy(CoroutineRunner.gameObject);
		}
	}
}