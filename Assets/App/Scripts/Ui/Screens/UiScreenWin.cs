using DG.Tweening;
using Root;
using TMPro;
using Ui.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens {
	public class UiScreenWin : UiScreenBlurredBack {
		[SerializeField] private Button _continueButton;
		[SerializeField] private Button _quitButton;
		[SerializeField] private Subtitle _subtitle;
		[SerializeField] private TextMeshProUGUI _title;

		public override void Init() {
			_continueButton.onClick.AddListener(HandleContinueButton);
			_quitButton.onClick.AddListener(HandleQuitButton);
			_subtitle.Init();
		}

		private void HandleContinueButton() {
			Core.StateController.SetState(GameStateMachine.StateType.NextLevel);
		}

		private void HandleQuitButton() {
			Core.StateController.SetState(GameStateMachine.StateType.QuitPlay);
		}

		protected override void PlayEnterAnim() {
			base.PlayEnterAnim();

			_title.alpha = 0f;
			_continueButton.transform.localScale = Vector3.zero;
			_quitButton.transform.localScale = Vector3.zero;
			_subtitle.Text.alpha = 0f;

			Sequence seq = DOTween.Sequence();

			seq.Insert(Constants.UiAnimDelay, _title.DOFade(1f, Constants.UiAnimDuration));
			seq.Insert(Constants.UiAnimDelay + Constants.UiAnimInterval, _continueButton.transform
				.DOScale(1f, Constants.UiAnimDuration).SetEase(Ease.OutBack));
			seq.Insert(Constants.UiAnimDelay + Constants.UiAnimInterval * 2, _quitButton.transform
				.DOScale(1f, Constants.UiAnimDuration).SetEase(Ease.OutBack));
			seq.Insert(Constants.UiAnimDelay + Constants.UiAnimInterval * 3, _subtitle.Text
				.DOFade(1f, Constants.UiAnimDuration));

			seq.SetUpdate(true);
		}
	}
}
