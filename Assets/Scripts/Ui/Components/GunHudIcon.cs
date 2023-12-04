using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Components {
    public class GunHudIcon : MonoBehaviour {
        [SerializeField] private RectTransform _body;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _frame;
        [SerializeField] private Color _selectedColor;

        private const float _inactiveAlpha = 0.5f;

        public void Init(Sprite sprite) {
            _icon.sprite = sprite;
            Deselect();
        }

        public void Select() {
            _canvasGroup.alpha = 1f;
            _body.DOKill();
            _body.DOScale(1.2f, 0.15f);
            _frame.color = _selectedColor;
        }

        public void Deselect() {
            _canvasGroup.alpha = _inactiveAlpha;
            _body.DOKill();
            _body.DOScale(1, 0.15f);
            _frame.color = Color.white;
        }
    }
}
