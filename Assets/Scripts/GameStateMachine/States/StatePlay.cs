using Root;
using Ui;
using UnityEngine;

namespace GameStateMachine.States {
	public class StatePlay : IState {
		public void Enter() {
			Core.UiController.Show(UiScreenType.Hud);
			Core.PointerController.Set(Pointer.PointerType.Gameplay);
		}

		public void Update() {
            Core.LevelController.Update();

			if (Input.GetKeyDown(KeyCode.Escape)) {
				Core.StateController.SetState(StateType.Pause);
			}
		}

		public void Exit() {
			Core.PointerController.Set(Pointer.PointerType.Ui);
		}
	}
}