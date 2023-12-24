using DG.Tweening;
using UnityEngine;

namespace Ui.Components.Hud {
	public class HudDashDot : MonoBehaviour {
		[SerializeField] private RectTransform _body;
		[SerializeField] private CanvasGroup _canvasGroup;

		private bool _isActive = true;

		public void Hide() {
			_canvasGroup.alpha = 0f;
		}

		public void SetActive(bool isActive) {
			if(_isActive == isActive) return; 

			_isActive = isActive;
			if(_isActive) Activate();
			else Deactivate();
		}

		private void Activate() {
			_canvasGroup.DOComplete();
			_body.DOComplete();

			_canvasGroup.alpha = 0f;
			_body.localScale = Vector3.zero;

			_canvasGroup.DOFade(1f, 0.1f);
			_body.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
		}

		private void Deactivate() {
			_canvasGroup.DOComplete();
			_body.DOComplete();

			_canvasGroup.DOFade(0f, 0.3f);
			_body.DOScale(0f, 0.1f).SetEase(Ease.InBack);
		}
	}
}
