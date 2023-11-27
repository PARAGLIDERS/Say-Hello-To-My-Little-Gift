#if UNITY_EDITOR
using UnityEditor;
#endif

using GameStateMachine;
using Root;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens {
	public class UiScreenMain : UiScreen {
		[SerializeField] private Button _play;
		[SerializeField] private Button _settings;
		[SerializeField] private Button _credits;
		[SerializeField] private Button _quit;
		
		public override void Init() {
			_play.onClick.AddListener(HandlePlay);
			_settings.onClick.AddListener(HandleSettings);
			_credits.onClick.AddListener(HandleCredits);
			_quit.onClick.AddListener(HandleQuit);
		}

		private void HandlePlay() {
			Core.StateController.SetState(StateType.LoadLevel);
		}

		private void HandleSettings() {
			Core.UiController.Show(UiScreenType.Settings);
		}

		private void HandleCredits() {
			Core.UiController.Show(UiScreenType.Credits);
		}

		private void HandleQuit() {
#if UNITY_EDITOR
			EditorApplication.isPlaying = false;
#endif
			Application.Quit();
		}
	}
}