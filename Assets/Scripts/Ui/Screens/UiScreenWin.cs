using Root;
using Ui.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens {
	public class UiScreenWin : UiScreenBlurredBack {
		[SerializeField] private Button _continueButton;
		[SerializeField] private Button _quitButton;
		[SerializeField] private Subtitle _subtitle;

		public override void Init() {
			_continueButton.onClick.AddListener(HandleContinueButton);
			_quitButton.onClick.AddListener(HandleQuitButton);
			_subtitle.Init();
		}

		private void HandleContinueButton() {
			Core.StateController.SetState(GameStateMachine.StateType.LoadLevel);
		}

		private void HandleQuitButton() {
			Core.StateController.SetState(GameStateMachine.StateType.QuitPlay);
		}
	}
}
