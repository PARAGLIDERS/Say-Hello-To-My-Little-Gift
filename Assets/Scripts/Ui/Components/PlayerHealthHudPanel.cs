using DG.Tweening;
using Player;
using Root;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Components {
	public class PlayerHealthHudPanel : MonoBehaviour {
		[SerializeField] private RectTransform _body;
		[SerializeField] private TextMeshProUGUI _value;
		[SerializeField] private ProgressBar _progress;
		[SerializeField] private PlayerHealthBarFace _face;

		private PlayerController _player => Core.LevelController.Player;
		private Sequence _damageSequence;

		public void Init() {
			_progress.Init(_player.CurrentHealth, _player.MaxHealth);
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
			_damageSequence.Insert(0.0f, _progress.GetUpdateSequence(_player.CurrentHealth));

			_value.text = _player.CurrentHealth.ToString();
		}

		private void OnHeal() {
			_damageSequence?.Kill();
			_progress.SetValue(_player.CurrentHealth);
			_value.text = _player.CurrentHealth.ToString();
		}
	}
}
