using DG.Tweening;
using Root;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Components.Hud {
	public abstract class HudVignette : MonoBehaviour {
		[SerializeField] private Image _image;
		[SerializeField] private float _alpha;

		public void Init() {
			ResetAlpha();
			HandleInit();
		}

		private void OnDestroy() {
			HandleDestroy();
		}

		protected void Activate() {
			_image.DOComplete();
			_image
				.DOFade(_alpha, 0.1f)
				.OnComplete(() => _image.DOFade(0f, 0.1f));
		}

		private void ResetAlpha() {
			_image.color = _image.color.With(a: 0);
		}

		protected abstract void HandleInit();
		protected abstract void HandleDestroy();
	}
}
