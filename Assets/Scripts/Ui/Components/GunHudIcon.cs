using DG.Tweening;
using GunSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Components {
    public class GunHudIcon : MonoBehaviour {
        [SerializeField] private RectTransform _body;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _ammo;
        [SerializeField] private GameObject _infinity;
        [SerializeField] private float _deselectedAlpha = 0.5f;
        [SerializeField] private Color _noAmmoColor = Color.red;

        public void Init(Sprite sprite) {
            _icon.sprite = sprite;
            Deselect();
        }

        public void Select() {
            _canvasGroup.alpha = 1f;
            _body.DOKill();
            _body.DOScale(1.2f, 0.15f);
        }

        public void Deselect() {
            _canvasGroup.alpha = _deselectedAlpha;
            _body.DOKill();
            _body.DOScale(1, 0.15f);
        }

        public void UpdateAmmo(Gun gun) {
            _ammo.text = gun.Ammo.ToString();
            _ammo.color = gun.Ammo <= 0 ? _noAmmoColor : Color.white;
			_ammo.gameObject.SetActive(!gun.IsInfinite);
			_infinity.gameObject.SetActive(gun.IsInfinite);
		}
    }
}
