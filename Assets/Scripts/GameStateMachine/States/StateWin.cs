using Root;
using UnityEngine;

namespace GameStateMachine.States {
	public class StateWin : IState {
		public void Enter() {
			Core.LevelController.Win();
			Core.MusicController.Stop(); // play some win sound
			Time.timeScale = 0f;
			Core.UiController.Show(Ui.UiScreenType.Win);
		}

		public void Exit() {
			Time.timeScale = 1f;
		}

		public void Update() {}
	}
}
