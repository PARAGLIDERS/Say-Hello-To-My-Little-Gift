using Music;
using Root;

namespace GameStateMachine.States {
	public class StateLoadLevel : StateBaseLoadScene {
		protected override int _sceneIndex => 2;
		protected override StateType _nextState => StateType.Play;

		public override void Exit() {
            Core.LevelController.Start();			
			Core.MusicController.Play(MusicClipType.Level1);
			base.Exit();
		}
	}
}