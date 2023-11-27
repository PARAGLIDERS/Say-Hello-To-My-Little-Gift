namespace GameStateMachine.States {
	public class StateBoot : StateBaseLoadScene {
		protected override int _sceneIndex => 1;
		protected override StateType _nextState => StateType.Main;
	}
}