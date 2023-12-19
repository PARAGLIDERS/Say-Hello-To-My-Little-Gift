using DG.Tweening;
using TMPro;
using UnityEngine;
using Utils;

namespace Ui.Components.Hud {
	public class HudPickupEventPanelItem : MonoBehaviour {
		[SerializeField] private TextMeshProUGUI _text;
		
		private const float _lifetime = 3f;

		private float _timer;
		private bool _active;

		public void Init() {
			_text.color = _text.color.With(a: 0);
		}

		public void Activate(string pickupName, int pickupCount, Color pickupColor) {
			_text.color = pickupColor;
			_text.text = $"{pickupName} +{pickupCount}";
			_text.DOKill();
			_text.DOFade(1f, 0.1f);

			transform.SetAsFirstSibling();
			_timer = Time.time + _lifetime;
			_active = true;
		}

		public void Deactivate() {
			_text.DOKill();
			_text.DOFade(0f, 0.3f);
			_active = false;
		}

		private void Update() {
			if (!_active) return;
			if (Time.time < _timer) return;

			Deactivate();
		}
	}
}
