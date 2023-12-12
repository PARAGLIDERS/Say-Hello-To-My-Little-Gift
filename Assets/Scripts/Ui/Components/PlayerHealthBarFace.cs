using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Components {
	public class PlayerHealthBarFace : MonoBehaviour {
		[SerializeField] private Image _image;
		[SerializeField] private List<PlayerFace> _items;

		public void Init() {
			_image.sprite = _items[0].Sprite;
		}

		public void UpdateImage(float value) {
			Sprite sprite = null;

			for (int i = 0; i < _items.Count; i++) {
				if (_items[i].MinValue < value) {
					sprite = _items[i].Sprite;
				}
			}

			if (sprite == null) {
				Debug.LogError($"face not found, value:{value}");
				return;
			}


		}
	}

	[Serializable]
	public struct PlayerFace {
		[SerializeField] private Sprite _sprite;
		[SerializeField] private Sprite _transitionSprite;
		[SerializeField] private float _minValue;

		public Sprite Sprite => _sprite;
		public Sprite TransitionSprite => _transitionSprite;
		public float MinValue => _minValue;
	}
}
