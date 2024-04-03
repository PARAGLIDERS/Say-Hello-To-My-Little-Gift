using DG.Tweening;
using Root;
using TMPro;
using Ui.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens {
	public class UiScreenCredits : UiScreenBlurredBack {
		[SerializeField] private Button _close;
		[SerializeField] private Button _next;
		[SerializeField] private GameObject _team;
		[SerializeField] private GameObject _assets;

		[Header("anim team")]
		[SerializeField] private CanvasGroup _teamTitle;
		[SerializeField] private CreditCard[] _cards;

		[Header("anim assets")]
		[SerializeField] private TextMeshProUGUI _assetsTitle;
		[SerializeField] private TextMeshProUGUI _note;
		[SerializeField] private RectTransform[] _assetsElements;

		public override void Init() {
			_assets.SetActive(false);
			_team.SetActive(true);

			_close.onClick.AddListener(HandleClose);
			_next.onClick.AddListener(HandleNext);
		}

		private void HandleNext() {
			_assets.SetActive(true);
			_team.SetActive(false);
			PlayAssetsAnim();
		}

		private void HandleClose() {
			Core.UiController.Show(UiScreenType.Main);
		}

		protected override void PlayEnterAnim() {
			base.PlayEnterAnim();

			Sequence seq = DOTween.Sequence();

			_teamTitle.alpha = 0f;
			seq.Insert(Constants.UiAnimDelay, _teamTitle.DOFade(1f, Constants.UiAnimDuration));
			
			for (int i = 0; i < _cards.Length; i++) {
				CreditCard card = _cards[i];
				float cardScaleTime = Constants.UiAnimDelay + Constants.UiAnimInterval * i;
				float cardDropTime = cardScaleTime + Constants.UiAnimDuration + Constants.UiAnimInterval;

				card.transform.localScale = Vector3.zero;
				seq.Insert(cardScaleTime, card.transform.DOScale(1f, Constants.UiAnimDuration).SetEase(Ease.OutBack));
				seq.Insert(cardDropTime, card.GetAnim());
			}

			_next.transform.localScale = Vector3.zero;
			seq.Append(_next.transform.DOScale(1f, Constants.UiAnimDuration).SetEase(Ease.OutBack));
		}

		private void PlayAssetsAnim() {
			Sequence seq = DOTween.Sequence();

			_assetsTitle.alpha = 0f;
			_note.alpha = 0f;

			seq.Insert(Constants.UiAnimDelay, _assetsTitle.DOFade(1f, Constants.UiAnimDuration));
			seq.Insert(Constants.UiAnimDelay + Constants.UiAnimInterval, _note.DOFade(1f, Constants.UiAnimDuration));

			for (int i = 0; i < _assetsElements.Length; i++) {
				_assetsElements[i].localScale = Vector3.zero;
				seq.Insert(Constants.UiAnimDelay + Constants.UiAnimInterval * i,
					_assetsElements[i].DOScale(1f, Constants.UiAnimDuration).SetEase(Ease.OutBack));
			}
		}
	}
}
