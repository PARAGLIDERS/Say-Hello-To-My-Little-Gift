using GunSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Components.GunWheel {
	public class GunWheelItem : MonoBehaviour {
		[SerializeField] private Image _fill;
		[SerializeField] private Image _icon;
		[SerializeField] private TextMeshProUGUI _ammo;
		[SerializeField] private Color _noAmmoColor = Color.red;
		[SerializeField] private GameObject _infinity;

		public Vector2 Direction => transform.up;
		private const float _inactiveAlpha = 0.1f;
		private const float _activeAlpha = 0.5f;

		public void Init(Sprite icon, int ammo, float fill, float angle, float fillAngle, bool isInfinite) {
			if(icon != null) _icon.sprite = icon;
			
			_fill.color = Color.white.With(a: .5f);
			_fill.fillAmount = fill;
			_fill.transform.localEulerAngles = new Vector3(0f, 0f, fillAngle);
			
			transform.localEulerAngles = new Vector3(0f, 0f, angle);
			_icon.transform.localEulerAngles = -transform.localEulerAngles;

			_ammo.text = ammo.ToString();
			_ammo.color = ammo <= 0 ? _noAmmoColor : Color.white;
			_ammo.gameObject.SetActive(!isInfinite);
			_infinity.gameObject.SetActive(isInfinite);
		}

		public void Activate() {
			_fill.color = Color.white.With(a: _activeAlpha);
		}

		public void Deactivate() {
			_fill.color = Color.white.With(a: _inactiveAlpha);
		}
	}
}
