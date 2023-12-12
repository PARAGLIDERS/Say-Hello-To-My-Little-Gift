using Root;

namespace GameStateMachine.States {
	public class StateNextLevel : IState {
		public void Enter() {
			Core.LevelController.Stop();

			StateType nextState = Core.LevelController.HasNextLevel() ? StateType.LoadLevel : StateType.Ending;
			Core.StateController.SetState(nextState);
		}

		public void Exit() {
			
		}

		public void Update() {
			
		}
	}
}
