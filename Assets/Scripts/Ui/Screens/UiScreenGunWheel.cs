using UnityEngine;
using Root;
using Components.GunWheel;
using System.Collections.Generic;
using System;
using GunSystem;

namespace Ui.Screens {
	public class UiScreenGunWheel : UiScreenBlurredBack {
		[SerializeField] private GunWheelItem _itemPrefab;
		[SerializeField] private RectTransform _container;
		[SerializeField] private List<GunWheelIcon> _icons;

		private List<Gun> _guns => Core.LevelController.GunsController.Guns;
		private Camera _camera => Core.UiController.Canvas.worldCamera;
		private List<GunWheelItem> _items = new List<GunWheelItem>();

		private int _selected;

		public override void Init() {

			float dAngle = 360f / _guns.Count;
			float offset = dAngle / 2;
			float fill = dAngle / 360f;


			for (int i = 0; i < _guns.Count; i++) {
				GunWheelItem item = Instantiate(_itemPrefab, _container);
				Sprite icon = GetSprite(_guns[i].Type);
				item.Init(icon, _guns[i].Ammo, fill - 0.01f, -dAngle * i, offset, _guns[i].IsInfinite);
				_items.Add(item);
			}
		}

		private Sprite GetSprite(GunType type) {
			GunWheelIcon icon = _icons.Find(x => x.Type == type);
			if (icon == null) return null;
			return icon.Sprite;
		}

		private void Update() {
			RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform,
				Input.mousePosition, _camera, out Vector2 movePos);

			Vector3 position = transform.TransformPoint(movePos);
			Vector3 direction = position - _container.position;

			int index = 0;
			float minAngle = 360;

			for (int i = 0; i < _items.Count; i++) {
				GunWheelItem item = _items[i];
				item.Deactivate();

				float angle = Mathf.Abs(Vector2.Angle(direction, item.Direction));

				if (angle < minAngle) {
					index = i;
					minAngle = angle;
				}
			}

			_items[index].Activate();
			_selected = index;
		}

		public override void Exit() {
			base.Exit();
			GunType type = _guns[_selected].Type;
			Core.LevelController.GunsController.SwitchTo(type);
		}
	}

	[Serializable]
	public class GunWheelIcon {
		public GunType Type;
		public Sprite Sprite;
	}
}
