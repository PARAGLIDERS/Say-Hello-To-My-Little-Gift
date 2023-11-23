using Misc.Root;

namespace Misc.GameStateMachine.States {
	public class StateLoadLevel : StateBaseLoadScene {
		protected override int _sceneIndex => 2;
		protected override StateType _nextState => StateType.Play;

		public override void Exit() {
			base.Exit();
			Core.LevelController.StartLevel();
		}
	}
}