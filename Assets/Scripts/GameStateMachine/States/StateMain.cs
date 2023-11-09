using Misc.Root;
using Ui;

namespace Misc.GameStateMachine.States {
	public class StateMain : IState {
		public void Enter() {
			Core.UiController.Show(UiScreenType.Main);
		}

		public void Update() { }
		public void Exit() { }
	}
}