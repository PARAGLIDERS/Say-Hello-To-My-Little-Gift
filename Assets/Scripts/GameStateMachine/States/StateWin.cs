using Root;

namespace GameStateMachine.States {
	public class StateWin : StateFinishLevel {
		protected override void HandleBeforeEnterDelay() {
			Core.LevelController.Win();
			Core.MusicController.Play(Music.MusicClipType.Win, false);
			Core.PostProcessingController.SetActiveExposure(true);
		}

		protected override void HandleAfterEnterDelay() {
			Core.UiController.Show(Ui.UiScreenType.Win);
		}

		protected override void HandleExit() {
			Core.PostProcessingController.SetActiveExposure(false);
		}
	}
}
