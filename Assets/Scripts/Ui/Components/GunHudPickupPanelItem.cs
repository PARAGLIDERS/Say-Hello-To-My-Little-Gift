using DG.Tweening;
using TMPro;
using UnityEngine;
using Utils;

namespace Ui.Components {
	public class GunHudPickupPanelItem : MonoBehaviour {
		[SerializeField] private TextMeshProUGUI _text;
		[SerializeField] private float _lifetime;

		private float _timer;
		private bool _active = false;

		public void Init() {
			_text.color = _text.color.With(a: 0);
		}

		public void Activate(string gunName, int pickupAmmo) {
			_text.text = $"{gunName} +{pickupAmmo}";
			_text.DOKill();
			_text.DOFade(1f, 0.15f);

			transform.SetAsLastSibling();
			_timer = Time.time + _lifetime;
			_active = true;
		}

		public void Deactivate() {
			_text.DOKill();
			_text.DOFade(0f, 0.15f);
			_active = false;
		}

		private void Update() {
			if(!_active) return;
			if (Time.time < _timer) return;

			Deactivate();
		}
	}
}
