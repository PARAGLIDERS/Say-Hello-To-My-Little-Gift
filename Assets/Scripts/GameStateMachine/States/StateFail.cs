using Root;
using System.Threading.Tasks;
using UnityEngine;

namespace GameStateMachine.States {
	public class StateFail : IState {
		public async void Enter() {
			Time.timeScale = 0.3f;
			Core.LevelController.Pause();
			Core.MusicController.Play(Music.MusicClipType.PlayerDeath, false);
			await Task.Delay(2000);
			Core.UiController.Show(Ui.UiScreenType.Fail);
		}

		public void Exit() {
			Time.timeScale = 1f;
		}

		public void Update() {}
	}
}
