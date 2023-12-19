using DG.Tweening;
using Level;
using Root;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

namespace Ui.Components {
	public class ChooseLevelItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
		[SerializeField] private Image _icon;
		[SerializeField] private TextMeshProUGUI _title;
		[SerializeField] private TextMeshProUGUI _description;
		[SerializeField] private Image _block;
		[SerializeField] private Image _select;

		private Action _onClick;
		private bool _isAvailable;
		private Vector3 _initScale;

		public void Init(LevelsConfigItem item, bool available, Action onClick) {
			_onClick = onClick;
			_icon.sprite = item.Icon;
			_title.text = item.Name;
			_description.text = item.Description;
			_block.gameObject.SetActive(!available);
			_isAvailable = available;
			_select.color = _select.color.With(a: 0f);
			_initScale = transform.localScale;
		}

		public void OnPointerClick(PointerEventData eventData) {
			if(!_isAvailable) return;
			_onClick?.Invoke();
			Core.SfxController.Play(SfxSystem.SfxType.UiButtonPress);
		}

		public void OnPointerEnter(PointerEventData eventData) {
			if(!_isAvailable) return;
			_select.DOFade(1f, 0.1f).SetUpdate(true);
			transform.DOScale(_initScale * 1.05f, 0.1f).SetUpdate(true);
			Core.SfxController.Play(SfxSystem.SfxType.UiButtonSelect);
		}

		public void OnPointerExit(PointerEventData eventData) {
			if(!_isAvailable) return;
			_select.DOFade(0f, 0.1f).SetUpdate(true);
			transform.DOScale(_initScale * 1f, 0.1f).SetUpdate(true);
		}
	}
}
