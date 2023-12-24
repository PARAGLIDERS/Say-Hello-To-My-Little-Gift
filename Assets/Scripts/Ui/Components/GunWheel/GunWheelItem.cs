using DG.Tweening;
using GunSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Components.GunWheel {
	public class GunWheelItem : MonoBehaviour {
		[SerializeField] private Image _fill;
		[SerializeField] private Image _fillBack;
		[SerializeField] private Image _icon;
		[SerializeField] private CanvasGroup _iconCanvasGroup;
		[SerializeField] private TextMeshProUGUI _ammo;
		[SerializeField] private Color _noAmmoColor = Color.red;
		[SerializeField] private GameObject _infinity;

		public Vector2 Direction => transform.up;

		public void Init(Sprite icon, int ammo, float fill, float angle, float fillAngle, bool isInfinite) {
			if(icon != null) _icon.sprite = icon;
			
			_fill.fillAmount = fill;
			_fill.transform.localEulerAngles = new Vector3(0f, 0f, fillAngle);
			_fillBack.fillAmount = fill;
			_fillBack.transform.localEulerAngles = new Vector3(0f, 0f, fillAngle);
			
			transform.localEulerAngles = new Vector3(0f, 0f, angle);
			_icon.transform.localEulerAngles = -transform.localEulerAngles;

			_ammo.text = ammo.ToString();
			_ammo.color = ammo <= 0 ? _noAmmoColor : Color.white;
			_ammo.gameObject.SetActive(!isInfinite);
			_infinity.gameObject.SetActive(isInfinite);
			_iconCanvasGroup.alpha = isInfinite || ammo > 0 ? 1f : 0.5f;
		}

		public void Activate() {
			transform.DOComplete();
			transform.DOScale(1.05f, 0.1f).SetUpdate(true);
			_fillBack.gameObject.SetActive(true);
		}

		public void Deactivate() {
			transform.DOComplete();
			transform.DOScale(1f, 0.1f).SetUpdate(true);
			_fillBack.gameObject.SetActive(false);
		}
	}
}
