using Root;
using UnityEngine;

namespace GameStateMachine.States {
	public class StateFail : IState {
		public void Enter() {
			Core.LevelController.Pause();
			Core.UiController.Show(Ui.UiScreenType.Fail);
			Core.MusicController.Play(Music.MusicClipType.PlayerDeath, false);

			Time.timeScale = 0.3f;
		}

		public void Exit() {
			Time.timeScale = 1f;
		}

		public void Update() {}
	}
}
