using DG.Tweening;
using Player;
using Root;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Components {
	public class PlayerHealthHudPanel : MonoBehaviour {
		[SerializeField] private RectTransform _body;
		[SerializeField] private Slider _sliderMain;
		[SerializeField] private Slider _sliderBack;
		[SerializeField] private TextMeshProUGUI _value;

		private PlayerController _player => Core.LevelController.PlayerController;
		private Sequence _damageSequence;

		public void Init() {
			_sliderMain.maxValue = _player.MaxHealth;
			_sliderMain.value = _player.CurrentHealth;

			_sliderBack.maxValue = _player.MaxHealth;
			_sliderBack.value = _player.CurrentHealth;

			_value.text = _player.CurrentHealth.ToString();

			_player.OnDamage += OnDamage;
			_player.OnHeal += OnHeal;
		}

		private void OnDestroy() {
			_player.OnDamage -= OnDamage;
			_player.OnHeal -= OnHeal;
		}

		private void OnDamage() {
			_damageSequence?.Kill();
			_damageSequence = DOTween.Sequence();

			_damageSequence.Insert(0.0f, _body.DOShakePosition(0.3f, 30f, 30));
			_damageSequence.Insert(0.0f, _sliderMain.DOValue(_player.CurrentHealth, 0.1f));			
			_damageSequence.Insert(0.5f, _sliderBack.DOValue(_player.CurrentHealth, 0.5f));

			_value.text = _player.CurrentHealth.ToString();
		}

		private void OnHeal() {
			_damageSequence?.Kill();
			
			_sliderMain.value = _player.CurrentHealth;
			_sliderBack.value = _player.CurrentHealth;

			_value.text = _player.CurrentHealth.ToString();
		}
	}
}
