using Root;

namespace GameStateMachine.States {
	public class StateNextLevel : IState {
		public void Enter() {
			Core.LevelController.Stop();
			Core.StateController.SetState(StateType.LoadLevel);
		}

		public void Exit() {
			
		}

		public void Update() {
			
		}
	}
}
