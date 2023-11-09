using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens {
	public class UiScreenLoading : UiScreen{
		[SerializeField] private Slider _slider;

		public override void Init() {
			_slider.value = 0f;
			_slider.DOValue(1f, 1f);
		}
	}
}