#if UNITY_EDITOR
using UnityEditor;
#endif

using Root;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace Ui.Screens {
	public class UiScreenMain : UiScreen {
		[SerializeField] private Button _play;
		[SerializeField] private Button _settings;
		[SerializeField] private Button _credits;
		[SerializeField] private Button _quit;
		[SerializeField] private TextMeshProUGUI _version;

		[SerializeField] private RectTransform[] _animElements;

		public override void Init() {
			_play.onClick.AddListener(HandlePlay);
			_settings.onClick.AddListener(HandleSettings);
			_credits.onClick.AddListener(HandleCredits);
			_quit.onClick.AddListener(HandleQuit);

			_version.text = Core.Version;
		}

		private void Update() {
			if(Input.GetKeyDown(KeyCode.E)) {
				Core.UiController.Show(UiScreenType.Ending);
			}
		}

		private void HandlePlay() {
			Core.UiController.Show(UiScreenType.ChooseLevel);
		}

		private void HandleSettings() {
			Core.UiController.Show(UiScreenType.Settings, true);
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

		protected override void PlayEnterAnim() {
			base.PlayEnterAnim();
			
			Sequence seq = DOTween.Sequence();
			_version.alpha = 0f;

			for (int i = 0; i < _animElements.Length; i++) {
				seq.Insert(Constants.UiAnimDelay + Constants.UiAnimInterval * i, _animElements[i]
					.DOAnchorPosX(-500f, Constants.UiAnimDuration)
					.From(true)
					.SetEase(Ease.OutBack));
			}

			seq.Append(_version.DOFade(1f, Constants.UiAnimDuration));
		}
	}
}