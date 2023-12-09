using Root;
using Ui;
using UnityEngine;

namespace GameStateMachine.States {
	public class StatePlay : IState {
		public void Enter() {
			Core.UiController.Show(UiScreenType.Hud);
		}

		public void Update() {
            Core.LevelController.Update();

			if (Input.GetKeyDown(KeyCode.Escape)) {
				Core.StateController.SetState(StateType.Pause);
			}
		}

		public void Exit() {}
	}
}