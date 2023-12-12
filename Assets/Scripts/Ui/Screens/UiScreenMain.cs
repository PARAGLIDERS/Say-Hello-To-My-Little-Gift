#if UNITY_EDITOR
using UnityEditor;
#endif

using GameStateMachine;
using Root;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Ui.Screens {
	public class UiScreenMain : UiScreen {
		[SerializeField] private Button _play;
		[SerializeField] private Button _settings;
		[SerializeField] private Button _credits;
		[SerializeField] private Button _quit;
		[SerializeField] private TextMeshProUGUI _version;
		
		public override void Init() {
			_play.onClick.AddListener(HandlePlay);
			_settings.onClick.AddListener(HandleSettings);
			_credits.onClick.AddListener(HandleCredits);
			_quit.onClick.AddListener(HandleQuit);

			_version.text = Core.Version;
		}

		private void HandlePlay() {
			Core.UiController.Show(UiScreenType.ChooseLevel);
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