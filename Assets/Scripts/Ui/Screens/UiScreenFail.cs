using GameStateMachine;
using Root;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens {
	public class UiScreenFail : UiScreen {
		[SerializeField] private Button _restartButton;
		[SerializeField] private Button _quitButton;
		[SerializeField] private TextMeshProUGUI _subTitle;
		[SerializeField] private string[] _subtitleTexts;

		public override void Init() {
			_restartButton.onClick.AddListener(HandleRestart);
			_quitButton.onClick.AddListener(HandleQuit);

			InitSubtitle();
		}

		private void HandleQuit() {
			Core.StateController.SetState(StateType.QuitPlay);
		}

		private void HandleRestart() {
			Core.StateController.SetState(StateType.Restart);
		}

		private void InitSubtitle() {
			int random = Random.Range(0, _subtitleTexts.Length);
			_subTitle.text = _subtitleTexts[random];
		}
	}
}
