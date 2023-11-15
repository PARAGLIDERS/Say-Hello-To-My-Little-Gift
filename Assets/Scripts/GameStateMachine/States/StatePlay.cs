using Misc.Root;
using Ui;
using UnityEngine;

namespace Misc.GameStateMachine.States {
	public class StatePlay : IState {
		public void Enter() {
			Core.UiController.Show(UiScreenType.Hud);
		}

		public void Update() {
			if (Input.GetKeyDown(KeyCode.Escape)) {
				Core.StateController.SetState(StateType.Pause);
			}
		}

		public void Exit() {
		}
	}
}