using Root;
using UnityEngine;

namespace GameStateMachine.States {
	public class StateGunWheel : IState {
		public void Enter() {
			Time.timeScale = 0.3f;
			Core.UiController.Show(Ui.UiScreenType.GunWheel);
		}

		public void Exit() {
			Time.timeScale = 1.0f;
		}

		public void Update() {
			if(!Core.InputController.GetGunWheelInput()) {
				Core.StateController.SetState(StateType.Play);
			}
		}
	}
}
