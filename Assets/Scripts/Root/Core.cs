using Misc.GameStateMachine;
using Misc.LevelControl;
using PoolSystem;
using Ui;
using UnityEngine;

namespace Misc.Root {
	public static class Core {
		public static Transform Container { get; private set; }
		public static Resources Resources { get; private set; }
		public static UiController UiController { get; private set; }
		public static StateController StateController { get; private set; }
		public static LevelController LevelController { get; private set; }
		public static PoolController PoolController { get; private set; }
		public static CoroutineRunner CoroutineRunner { get; private set; }
		
		public static void Init(Transform parent, Resources resources) {
			Container = parent;
			CoroutineRunner = new GameObject("coroutine runner").AddComponent<CoroutineRunner>();
			CoroutineRunner.transform.SetParent(parent);
			
			Resources = resources;

			StateController = new StateController();
			UiController = new UiController();
			LevelController = new LevelController();
			PoolController = new PoolController();
			
			StateController.SetState(StateType.Boot);
		}

		public static void Update() {
			StateController?.Update();
		}

		public static void Dispose() {
			LevelController.Dispose();
			PoolController.Dispose();
			
			CoroutineRunner.StopAll();
			Object.Destroy(CoroutineRunner.gameObject);
		}
	}
}