using Root;
using System.Threading.Tasks;
using UnityEngine;

namespace GameStateMachine.States {
	public class StateFail : StateFinishLevel {
		protected override void HandleBeforeEnterDelay() {
			Core.MusicController.Play(Music.MusicClipType.PlayerDeath, false);
			Core.PostProcessingController.SetActiveGreyScale(true);
		}

		protected override void HandleAfterEnterDelay() {
			Core.UiController.Show(Ui.UiScreenType.Fail);
		}

		protected override void HandleExit() {
			Core.PostProcessingController.SetActiveGreyScale(false);
		}
	}
}
