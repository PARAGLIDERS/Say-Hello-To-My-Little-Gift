using Misc.GameStateMachine;
using Misc.LevelControl;
using Ui;
using UnityEngine;

namespace Misc.Root {
	public static class Core {
		public static Resources Resources { get; private set; }
		public static UiController UiController { get; private set; }
		public static StateController StateController { get; private set; }
		public static LevelController LevelController { get; private set; }
		
		public static void Init(Transform parent, Resources resources) {
			Resources = resources;

			StateController = new StateController();
			UiController = new UiController(parent);
			LevelController = new LevelController();
			
			StateController.SetState(StateType.Boot);
		}

		public static void Update() {
			StateController?.Update();
		}
	}
}