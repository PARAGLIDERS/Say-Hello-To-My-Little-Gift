using DG.Tweening;
using GameStateMachine;
using Root;
using TMPro;
using Ui.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens {
	public class UiScreenFail : UiScreenBlurredBack {
		[SerializeField] private Button _restartButton;
		[SerializeField] private Button _quitButton;
		[SerializeField] private Subtitle _subtitle;

		[SerializeField] private TextMeshProUGUI _title;

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

		protected override void PlayEnterAnim() {
			base.PlayEnterAnim();

			_title.alpha = 0f;
			_restartButton.transform.localScale = Vector3.zero;
			_quitButton.transform.localScale = Vector3.zero;
			_subtitle.Text.alpha = 0f;

			Sequence seq = DOTween.Sequence();
			
			seq.Insert(Constants.UiAnimDelay, _title.DOFade(1f, Constants.UiAnimDuration));
			seq.Insert(Constants.UiAnimDelay + Constants.UiAnimInterval, _restartButton.transform
				.DOScale(1f, Constants.UiAnimDuration).SetEase(Ease.OutBack));
			seq.Insert(Constants.UiAnimDelay + Constants.UiAnimInterval * 2, _quitButton.transform
				.DOScale(1f, Constants.UiAnimDuration).SetEase(Ease.OutBack));
			seq.Insert(Constants.UiAnimDelay + Constants.UiAnimInterval * 3, _subtitle.Text
				.DOFade(1f, Constants.UiAnimDuration));

			seq.SetUpdate(true);
		}
	}
}
