using DG.Tweening;
using Root;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

namespace Ui {
	public class UiButton : Button {
		private Image _image;
		private const float _fade = 1f;
		private const float _scale = 1.1f;
		
		protected override void Awake() {
			base.Awake();
			_image = GetComponent<Image>();
			_image.color = _image.color.With(a: 0);
		}

		public override void OnPointerClick(PointerEventData eventData) {
			base.OnPointerClick(eventData);
            Core.SfxController.Play(SfxSystem.SfxType.UiButtonPress);
		}

		public override void OnPointerEnter(PointerEventData eventData) {
			base.OnPointerEnter(eventData);
			transform.DOScale(_scale, 0.1f).SetUpdate(true);
			_image.DOFade(_fade, 0.1f).SetUpdate(true);
            Core.SfxController.Play(SfxSystem.SfxType.UiButtonSelect);
        }

        public override void OnPointerExit(PointerEventData eventData) {
			base.OnPointerExit(eventData);
			transform.DOScale(1f, 0.1f).SetUpdate(true);
			_image.DOFade(0f, 0.1f).SetUpdate(true);
		}

		public override void OnPointerDown(PointerEventData eventData) {
			base.OnPointerDown(eventData);
			transform.DOScale(1f, 0.03f).SetUpdate(true)
				.OnComplete(() => transform.DOScale(_scale, 0.1f).SetUpdate(true));
		}

		protected override void OnDestroy() {
            base.OnDestroy();
            transform.DOKill();
            _image.DOKill();
        }
    }
}