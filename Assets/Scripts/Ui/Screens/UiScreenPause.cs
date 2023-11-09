using Misc.GameStateMachine;
using Misc.Root;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens {
	public class UiScreenPause : UiScreen{
		[SerializeField] private Button _back;
		[SerializeField] private Button _quit;

		public override void Init() {
			_back.onClick.AddListener(HandleBack);
			_quit.onClick.AddListener(HandleQuit);
		}

		private void HandleBack() {
			Core.StateController.SetState(StateType.Play);
		}

		private void HandleQuit() {
			Core.StateController.SetState(StateType.QuitPlay);
		}
	}
}