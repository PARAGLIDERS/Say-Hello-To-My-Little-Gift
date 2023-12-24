using Root;

namespace GameStateMachine.States {
	public class StateNextLevel : IState {
		public void Enter() {
			if (Core.LevelController.HasNextLevel()) {
				Core.LevelController.Stop();
				Core.StateController.SetState(StateType.LoadLevel);
			} else {
				Core.StateController.SetState(StateType.Ending);
			}
		}

		public void Exit() {
			
		}

		public void Update() {
			
		}
	}
}
