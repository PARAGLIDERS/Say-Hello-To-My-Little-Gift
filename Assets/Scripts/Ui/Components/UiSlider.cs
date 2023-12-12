using Root;
using SfxSystem;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ui.UiKit {
	public class UiSlider : MonoBehaviour, IPointerClickHandler {
		[SerializeField] private Slider _slider;
		[SerializeField] private TextMeshProUGUI _valueText;		

		public void Init(float current, float min, float max, Action<float> onValueChanged) {
			_slider.minValue = min;
			_slider.maxValue = max;
			_slider.value = current;
			_slider.onValueChanged.AddListener(value => {
				UpdateValueText();
				onValueChanged.Invoke(value);
			});

			UpdateValueText();
		}

		private void UpdateValueText() {
			float value = _slider.value;
			float min = _slider.minValue;
			float max = _slider.maxValue;
			float percentage = Mathf.InverseLerp(min, max, value);
			_valueText.text = $"{(int)(percentage * 100f)}%";
		}

		public void OnPointerClick(PointerEventData eventData) {
			Core.SfxController.Play(SfxType.UiButtonPress);
		}
	}
}