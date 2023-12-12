using Root;

namespace Ui.Screens {
	public abstract class UiScreenBlurredBack : UiScreen {
		public override void Enter() {
			base.Enter();
			Core.PostProcessingController.SetActiveDepthOfField(true);
		}

		public override void Exit() {
			base.Exit();
			Core.PostProcessingController.SetActiveDepthOfField(false);
		}
	}
}
