using DG.Tweening;
using Root;
using SfxSystem;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

namespace Ui.UiKit {
	public class UiToggle : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
		[SerializeField] private Toggle _toggle;
		[SerializeField] private Image _image;

		private const float _scale = 1.05f;

		public void Init(bool value, Action<bool> onValueChanged) { 
			_toggle.isOn = value;
			_toggle.onValueChanged.AddListener(onValueChanged.Invoke);
			_image.color = _image.color.With(a: 0);
		}

		public void OnPointerClick(PointerEventData eventData) {
			Core.SfxController.Play(SfxType.UiButtonPress);
		}
		
		public void OnPointerEnter(PointerEventData eventData) {
			Core.SfxController.Play(SfxType.UiButtonSelect);
			transform.DOScale(_scale, 0.1f).SetUpdate(true);
			_image.DOFade(1f, 0.1f).SetUpdate(true);
		}

		public void OnPointerExit(PointerEventData eventData) {
			transform.DOScale(1f, 0.1f).SetUpdate(true);
			_image.DOFade(0f, 0.1f).SetUpdate(true);
		}
	}
}