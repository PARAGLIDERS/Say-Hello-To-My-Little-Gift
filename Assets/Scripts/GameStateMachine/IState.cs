namespace Misc.GameStateMachine {
	public interface IState {
		void Enter();
		void Update();
		void Exit();
	}
}