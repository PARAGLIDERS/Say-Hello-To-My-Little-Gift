using Root;
using UnityEngine;

namespace GameStateMachine.States {
	public class StateEnding : IState {
		public void Enter() {
			Time.timeScale = 0.0f;
			Core.UiController.Show(Ui.UiScreenType.Ending);
		}

		public void Exit() {
			Core.LevelController.Stop();
			Time.timeScale = 1f;
		}
		public void Update() { }
	}
}
