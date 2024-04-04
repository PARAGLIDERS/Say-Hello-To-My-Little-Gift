using Root;
using Ui;

namespace GameStateMachine.States {
	public class StatePlay : IState {
		public void Enter() {
			Core.UiController.Show(UiScreenType.Hud);
		}

		public void Update() {
            Core.LevelController.Update();

			if (Core.InputController.GetEscapeInput()) {
				Core.StateController.SetState(StateType.Pause);
			}else if(Core.InputController.GetGunWheelInput()) {
				Core.StateController.SetState(StateType.GunWheel);
			}
		}

		public void Exit() {
			Core.PointerController.Set(Pointer.PointerType.Ui);
		}
	}
}