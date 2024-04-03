using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Components {
	public class ProgressBar : MonoBehaviour {
		[SerializeField] private Slider _sliderMain;
		[SerializeField] private Slider _sliderBack;

		private Sequence _sequence;

		public void Init(int current, int max) {
			_sliderMain.maxValue = max;
			_sliderMain.value = current;

			_sliderBack.maxValue = max;
			_sliderBack.value = current;

			_sequence = DOTween.Sequence(); 
		}

		public Sequence GetUpdateSequence(int value) {
			_sliderBack.DOComplete();
			_sliderMain.DOComplete();
			
			_sequence?.Complete();
			_sequence.Insert(0.0f, _sliderMain.DOValue(value, 0.1f));
			_sequence.Insert(0.5f, _sliderBack.DOValue(value, 0.5f));
			
			return _sequence;
		}

		public void SetValue(int value) {
			_sliderBack.DOComplete();
			_sliderMain.DOComplete();
			_sequence?.Complete();

			_sliderMain.value = value;
			_sliderBack.value = value;
		}
	}
}
