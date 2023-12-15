using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pointer {
	[CreateAssetMenu(menuName ="Santa/Pointer Controller Config")]
	public class PointerControllerConfig : ScriptableObject {
		[SerializeField] private Canvas _canvas;
		[SerializeField] private Image _pointerPrefab;
		[SerializeField] private List<PointerConfig> _items;

		public Canvas Canvas => _canvas;
		public Image PointerPrefab => _pointerPrefab;
		public List<PointerConfig> Items => _items;
	}

	[Serializable]
	public class PointerConfig {
		[SerializeField] private PointerType _type;
		[SerializeField] private Sprite _sprite;
		[SerializeField] private Vector2 _pivot;

		public PointerType Type => _type;
		public Sprite Sprite => _sprite;
		public Vector2 Pivot => _pivot;
	}
}
