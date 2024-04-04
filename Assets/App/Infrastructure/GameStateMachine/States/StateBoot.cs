using Data;
using Music;
using Pooling;
using Root;
using Ui;

namespace GameStateMachine.States {
	public class StateBoot : StateBaseLoadScene {
		protected override int _sceneIndex => 1;
		protected override StateType _nextState => StateType.Main;
        protected override bool _showLoadingScreen => false;
        protected override int _delay => 0;

		private readonly DataService _dataController;

		public override void Exit() {
			base.Exit();
			_dataController.ApplySettings();
		}
	}
}