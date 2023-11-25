using Misc.Root;

namespace Misc.GameStateMachine.States {
	public class StateQuitPlay : StateBaseLoadScene {
		protected override int _sceneIndex => 1;
		protected override StateType _nextState => StateType.Main;

		public override void Enter() {
			base.Enter();
			Core.EnemySpawner.Stop();
            Core.PoolController.DeactivateAll();
		}
	}
}