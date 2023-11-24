using EnemySpawning;
using Misc.GameStateMachine;
using PoolSystem;
using SfxSystem;
using Ui;
using UnityEngine;

namespace Misc.Root {
	public static class Core {
		public static Transform Container { get; private set; }
		public static UiController UiController { get; private set; }
		public static StateController StateController { get; private set; }
		public static PoolController PoolController { get; private set; }
        public static EnemySpawner EnemySpawner { get; private set; }
        public static SfxController SfxController { get; private set; }
		public static CoroutineRunner CoroutineRunner { get; private set; }
		
		public static void Init(Transform parent, Resources resources) {
			Container = parent;
			
            CoroutineRunner = new GameObject("coroutine runner").AddComponent<CoroutineRunner>();
			CoroutineRunner.transform.SetParent(parent);
			
            StateController = new StateController();
			UiController = new UiController(parent, resources.CanvasPrefab, resources.ScreenConfig);
            PoolController = new PoolController(Container, resources.PoolConfig);
            EnemySpawner = new EnemySpawner(resources.EnemySpawnerConfig, resources.EnemySpawnerGridConfig);
            SfxController = new SfxController(parent, resources.SfxConfig);
			StateController.SetState(StateType.Boot);
		}

		public static void Update() {
			StateController?.Update();
		}

		public static void Dispose() {
			PoolController.Dispose();
			
            EnemySpawner.Stop();
            EnemySpawner.Dispose();

			CoroutineRunner.StopAll();
			Object.Destroy(CoroutineRunner.gameObject);
		}
	}
}