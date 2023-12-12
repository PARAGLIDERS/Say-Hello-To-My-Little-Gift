using GameStateMachine;
using Root;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens {
	public class UiScreenPause : UiScreenBlurredBack {
		[SerializeField] private Button _back;
		[SerializeField] private Button _restart;
		[SerializeField] private Button _quit;

		public override void Init() {
			_back.onClick.AddListener(HandleBack);
			_restart.onClick.AddListener(HandleRestart);
			_quit.onClick.AddListener(HandleQuit);
		}

		private void HandleBack() {
			Core.StateController.SetState(StateType.Play);
		}

		private void HandleRestart() {
			Core.StateController.SetState(StateType.Restart);
		}

		private void HandleQuit() {
			Core.StateController.SetState(StateType.QuitPlay);
		}
	}
}