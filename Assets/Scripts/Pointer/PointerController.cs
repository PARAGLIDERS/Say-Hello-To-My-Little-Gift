using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pointer {
	public class PointerController {
		private readonly Canvas _canvas;
		private readonly Dictionary<PointerType, PointerConfig> _pointers;
		private readonly Image _pointer;

		public PointerController(Transform parent, PointerControllerConfig config) {
			Cursor.visible = false;

			_canvas = Object.Instantiate(config.Canvas, parent);
			_pointer = Object.Instantiate(config.PointerPrefab, _canvas.transform);

			_pointers = new Dictionary<PointerType, PointerConfig>();
			foreach (var p in config.Items) {
				_pointers.TryAdd(p.Type, p);
			}

			_pointer.transform
				.DOScale(_pointer.transform.localScale * 1.2f, 0.7f)
				.SetLoops(-1, LoopType.Yoyo)
				.SetUpdate(true);
		}

		public void Set(PointerType pointerType) {
			if(!_pointers.TryGetValue(pointerType, out PointerConfig config)) {
				Debug.LogError($"pointer not found: {pointerType}");
				return;
			}

			_pointer.sprite = config.Sprite;
			_pointer.rectTransform.pivot = config.Pivot;
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
		Ui = 1,
		Gameplay = 2,
	}
}
