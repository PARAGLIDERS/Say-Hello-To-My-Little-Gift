using Root;

namespace GameStateMachine.States {
	public class StateLoadLevel : StateBaseLoadScene {
		protected override int _sceneIndex => Core.LevelController.GetCurrentLevelConfig().SceneIndex;
		protected override StateType _nextState => StateType.Play;

		public override void Exit() {
            Core.LevelController.Start();			
			Core.MusicController.Play(Core.LevelController.GetCurrentLevelConfig().MusicType);
			base.Exit();
		}
	}
}