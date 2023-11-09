namespace Misc.GameStateMachine.States {
	public class StateQuitPlay : StateBaseLoadScene {
		protected override int _sceneIndex => 1;
		protected override StateType _nextState => StateType.Main;
	}
}