using Root;

namespace GameStateMachine.States {
	public class StateEnding : IState {
		public void Enter() {
			Core.UiController.Show(Ui.UiScreenType.Ending);
		}

		public void Exit() { }
		public void Update() { }
	}
}
