using Root;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens {
	public class UiScreenEnding : UiScreenBlurredBack {
		[SerializeField] private Button _continueButton;

		public override void Init() {
			_continueButton.onClick.AddListener(HandleContinueButton);
		}

		private void HandleContinueButton() {
			Core.StateController.SetState(GameStateMachine.StateType.QuitPlay);
		}
	}
}
