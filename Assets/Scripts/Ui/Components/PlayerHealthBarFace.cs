using DG.Tweening;
using Root;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Components {
	public class PlayerHealthBarFace : MonoBehaviour {
		[SerializeField] private RectTransform _body;
		[SerializeField] private Image _image;
		[SerializeField] private List<PlayerFace> _items;

		private const float _rotationMagnitude = 5f;
		private Sequence _switchSequence;
		private int _currentIndex;

		public void Init(float value) {
			_image.transform.localEulerAngles = new Vector3(0, 0, -_rotationMagnitude);
			_image.transform
				.DORotate(new Vector3(0, 0, _rotationMagnitude), 1f)
				.SetEase(Ease.InOutSine)
				.SetLoops(-1, LoopType.Yoyo);

			_image.sprite = _items[GetItemIndex(value)].Sprite;
		}

		public void UpdateImage(float value) {
			int index = GetItemIndex(value);

			if (index == _currentIndex)	return;

			if (index < _currentIndex) SetFace(_items[index].Sprite);
			else SwitchFace(_items[index].TransitionSprite, _items[index].Sprite);

			_currentIndex = index;
		}

		private int GetItemIndex(float value) {
			int index = 0;

			for (int i = 0; i < _items.Count; i++) {
				if (_items[i].Value > value) {
					index = i;
				}
			}

			return index;
		}

		private void SwitchFace(Sprite transition, Sprite target) {
			_body.DOComplete();

			_switchSequence?.Kill();
			_switchSequence = DOTween.Sequence();

			const float time = 0.3f;
			_switchSequence
				.Insert(0f, _body.DOShakePosition(time, 1f, 20))
				.InsertCallback(0f, () => SetFace(transition))
				.InsertCallback(time, () => SetFace(target));
		}

		private void SetFace(Sprite face) {
			_image.sprite = face;
		}
	}

	[Serializable]
	public struct PlayerFace {
		[SerializeField] private Sprite _sprite;
		[SerializeField] private Sprite _transitionSprite;
		[SerializeField] private float _value;

		public Sprite Sprite => _sprite;
		public Sprite TransitionSprite => _transitionSprite;
		public float Value => _value;
	}
}
