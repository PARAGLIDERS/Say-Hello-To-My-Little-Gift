using GameStateMachine;
using Root;
using Ui.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens {
	public class UiScreenFail : UiScreenBlurredBack {
		[SerializeField] private Button _restartButton;
		[SerializeField] private Button _quitButton;
		[SerializeField] private Subtitle _subtitle;

		public override void Init() {
			_restartButton.onClick.AddListener(HandleRestart);
			_quitButton.onClick.AddListener(HandleQuit);
			_subtitle.Init();
		}

		private void HandleQuit() {
			Core.StateController.SetState(StateType.QuitPlay);
		}

		private void HandleRestart() {
			Core.StateController.SetState(StateType.Restart);
		}
	}
}
