using DG.Tweening;
using GunSystem;
using Root;
using TMPro;
using UnityEngine;
using Utils;

namespace Ui.Components.Hud {
	public class HudLowAmmoPanel : MonoBehaviour {
		[SerializeField] private TextMeshProUGUI _label;

		public void Init() {
			_label.color = _label.color.With(a: 0);
			_label.transform
				.DOScale(_label.transform.localScale * 1.1f, 0.3f)
				.SetLoops(-1, LoopType.Yoyo)
				.SetUpdate(true);

			Core.LevelController.GunsController.OnAmmoChange += UpdateLabel;
			Core.LevelController.GunsController.OnSwitch += UpdateLabel;
		}

		private void OnDestroy() {
			Core.LevelController.GunsController.OnAmmoChange -= UpdateLabel;
			Core.LevelController.GunsController.OnSwitch -= UpdateLabel;
		}

		private void UpdateLabel(Gun gun) {
			_label.color = _label.color.With(a: gun.LowAmmo ? 1 : 0);
		}
	}
}
