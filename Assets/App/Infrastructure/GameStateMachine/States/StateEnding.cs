using Level;
using Root;
using Ui;
using UnityEngine;

namespace GameStateMachine.States {
	public class StateEnding : IState {
		private readonly UiService _uiController;
		private readonly LevelController _levelController;

		public void Enter() {
			Time.timeScale = 0.0f;
			_uiController.Show(Ui.UiScreenType.Ending);
		}

		public void Exit() {
			_levelController.Stop();
			Time.timeScale = 1f;
		}
		public void Update() { }
	}
}
