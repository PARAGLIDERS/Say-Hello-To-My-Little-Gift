using DG.Tweening;
using Root;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens {
	public class UiScreenEnding : UiScreenBlurredBack {
		[SerializeField] private Button _continueButton;
		[SerializeField] private TextMeshProUGUI[] _texts;

		public override void Init() {
			_continueButton.onClick.AddListener(HandleContinueButton);
		}

		private void HandleContinueButton() {
			Core.StateController.SetState(GameStateMachine.StateType.QuitPlay);
		}

		protected override void PlayEnterAnim() {
			base.PlayEnterAnim();
			Sequence seq = DOTween.Sequence();
			
			_continueButton.transform.localScale = Vector3.zero;

			for (int i = 0; i < _texts.Length; i++) {
				_texts[i].alpha = 0f;
				seq.Insert(Constants.UiAnimDelay + Constants.UiAnimInterval * i,
					_texts[i].DOFade(1f, Constants.UiAnimDuration));
			}

			seq.Append(_continueButton.transform.DOScale(1f, Constants.UiAnimDuration).SetEase(Ease.OutBack));
			seq.SetUpdate(true);
		}
	}
}
