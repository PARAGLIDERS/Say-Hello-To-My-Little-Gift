using Root;
using UnityEngine;

namespace GameStateMachine.States {
	public class StateWin : IState {
		public void Enter() {
			Core.UiController.Show(Ui.UiScreenType.Win);
			Core.MusicController.Stop(); // play some win music
			Time.timeScale = 0f;
		}

		public void Exit() {
			Time.timeScale = 1f;
		}

		public void Update() {}
	}
}
