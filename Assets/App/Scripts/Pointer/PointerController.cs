using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pointer {
	public class PointerController {
		private readonly Canvas _canvas;
		private readonly Dictionary<PointerType, PointerConfig> _pointers;
		private readonly Image _pointer;
		
		private Tweener _pointerAnimation;

		public PointerController(Transform parent, PointerControllerConfig config) {
			Cursor.visible = false;

			_canvas = Object.Instantiate(config.Canvas, parent);
			_pointer = Object.Instantiate(config.PointerPrefab, _canvas.transform);

			_pointers = new Dictionary<PointerType, PointerConfig>();
			foreach (var p in config.Items) {
				_pointers.TryAdd(p.Type, p);
			}
		}

		public void Set(PointerType pointerType) {
			if(!_pointers.TryGetValue(pointerType, out PointerConfig config)) {
				Debug.LogError($"pointer not found: {pointerType}");
				return;
			}

			_pointerAnimation?.SetLoops(0);
			_pointerAnimation?.Complete();
			_pointerAnimation?.Kill();
			_pointer.transform.localScale = Vector3.one;

			_pointer.sprite = config.Sprite;
			_pointer.rectTransform.pivot = config.Pivot;
			_pointer.rectTransform.sizeDelta = Vector2.one * (config.Scale);


			if (config.Animated) {
				_pointerAnimation = _pointer.transform
					.DOScale(_pointer.transform.localScale * 1.2f, 0.7f)
					.SetLoops(-1, LoopType.Yoyo)
					.SetUpdate(true);
			}
		}

		public void Update() {
#if UNITY_EDITOR
			Cursor.visible = false;
#endif
			Vector2 movePos;

			RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform,
				Input.mousePosition, _canvas.worldCamera, out movePos);

			_pointer.transform.position = _canvas.transform.TransformPoint(movePos);
		}		
	}

	public enum PointerType {
		Ui = 0,
		Pistol = 1,
		Uzi = 2,
		Shotgun = 3,
		Auto = 4,
		DoubleShotgun = 5,
		RocketLauncher = 6,
		Minigun = 7,
	}
}
