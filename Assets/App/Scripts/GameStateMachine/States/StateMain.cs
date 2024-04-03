using Music;
using Root;
using Ui;

namespace GameStateMachine.States {
	public class StateMain : IState {
		public void Enter() {
			Core.UiController.Show(UiScreenType.Main);
			Core.MusicController.Play(MusicClipType.Menu);
		}

		public void Update() { }
		public void Exit() { }
	}
}