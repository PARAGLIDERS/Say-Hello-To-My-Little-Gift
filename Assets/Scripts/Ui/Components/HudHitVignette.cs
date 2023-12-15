using DG.Tweening;
using Root;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Ui.Components {
	public class HudHitVignette : MonoBehaviour {
		[SerializeField] private Image _image;
		[SerializeField] private float _alpha;

		public void Init() {
			ResetAlpha();
			Core.LevelController.Player.OnDamage += Activate;
		}

		private void OnDestroy() {
			Core.LevelController.Player.OnDamage -= Activate;
		}

		private void Activate() {
			_image.DOComplete();
			_image
				.DOFade(_alpha, 0.1f)
				.OnComplete(() => _image.DOFade(0f, 0.1f));
		}

		private void ResetAlpha() {
			_image.color = _image.color.With(a: 0);
		}
	}
}
