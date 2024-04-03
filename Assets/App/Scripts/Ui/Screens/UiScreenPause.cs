using DG.Tweening;
using GameStateMachine;
using Root;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens {
	public class UiScreenPause : UiScreenBlurredBack {
		[SerializeField] private Button _back;
		[SerializeField] private Button _restart;
		[SerializeField] private Button _settings;
		[SerializeField] private Button _quit;

		[SerializeField] private TextMeshProUGUI _title;
		[SerializeField] private RectTransform[] _animElements;

		public override void Init() {
			_back.onClick.AddListener(HandleBack);
			_restart.onClick.AddListener(HandleRestart);
			_settings.onClick.AddListener(HandleSettings);
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

		private void HandleSettings() {
			Core.UiController.Show(UiScreenType.Settings, true);
		}

		protected override void PlayEnterAnim() {
			base.PlayEnterAnim();

			Sequence seq = DOTween.Sequence();

			_title.alpha = 0f;
			seq.Insert(Constants.UiAnimDelay, _title.DOFade(1f, Constants.UiAnimDuration));

			for (int i = 0; i < _animElements.Length; i++) {
				_animElements[i].localScale = Vector3.zero;
				seq.Insert(Constants.UiAnimDelay + Constants.UiAnimInterval * i,
					_animElements[i].DOScale(1f, Constants.UiAnimDuration).SetEase(Ease.OutBack));
			}

			seq.SetUpdate(true);
		}
	}
}