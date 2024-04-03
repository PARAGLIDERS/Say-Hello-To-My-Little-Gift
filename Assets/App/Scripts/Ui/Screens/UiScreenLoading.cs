using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens {
	public class UiScreenLoading : UiScreen {
		[SerializeField] private Slider _slider;
		[SerializeField] private RectTransform _icon;

		public override void Init() {
			_slider.value = 0f;
			_slider.DOValue(1f, 1f).SetUpdate(true);
			_icon
				.DOScale(1.05f, .1f)
				.SetEase(Ease.InOutSine)
				.SetLoops(-1, LoopType.Yoyo)
				.SetUpdate(true);
		}

        private void OnDestroy() {
            _slider.DOKill();
        }
    }
}