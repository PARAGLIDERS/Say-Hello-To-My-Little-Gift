using Misc.Root;
using Ui;
using UnityEngine;

namespace Misc.GameStateMachine.States {
	public class StatePause : IState {
		public void Enter() {
			Time.timeScale = 0;
			Core.UiController.Show(UiScreenType.Pause);
		}

		public void Update() {
			if (Input.GetKeyDown(KeyCode.Escape)) {
				Core.StateController.SetState(StateType.Play);
			}
		}

		public void Exit() {
			Time.timeScale = 1;
		}
	}
}