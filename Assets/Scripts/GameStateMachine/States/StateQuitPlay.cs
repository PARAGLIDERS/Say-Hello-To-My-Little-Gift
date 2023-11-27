using Root;

namespace GameStateMachine.States {
	public class StateQuitPlay : StateBaseLoadScene {
		protected override int _sceneIndex => 1;
		protected override StateType _nextState => StateType.Main;

		public override void Enter() {
			Core.EnemySpawner.Stop();
            Core.PoolController.DeactivateAll();
			base.Enter();
		}
	}
}