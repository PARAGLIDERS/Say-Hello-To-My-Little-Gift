using GunSystem;
using Root;
using System.Collections.Generic;
using UnityEngine;

namespace Ui.Components {
	public class GunHudPickupPanel : MonoBehaviour {
		[SerializeField] private GunHudPickupPanelItem _itemPrefab;
		[SerializeField] private RectTransform _itemsContainer;
		[SerializeField] private int _maxItemsCount;

		private Queue<GunHudPickupPanelItem> _items;

		public void Init() {
			InitItems();
			SubscribeToEvents();
		}

		private void OnDestroy() {
			UnsubscribeFromEvents();
		}

		public void InitItems() {
			_items = new Queue<GunHudPickupPanelItem>();

			for (int i = 0; i < _maxItemsCount; i++) {
				GunHudPickupPanelItem item = Instantiate(_itemPrefab, _itemsContainer);
				item.Init();
				_items.Enqueue(item);
			}
		}

		private void SubscribeToEvents() {
			Core.LevelController.GunsController.OnPickup += OnPickup;
		}

		private void UnsubscribeFromEvents() {
			Core.LevelController.GunsController.OnPickup -= OnPickup;
		}

		private void OnPickup(IGun gun, int ammo) {
			GunHudPickupPanelItem item = _items.Dequeue();
			
			item.Deactivate();
			item.Activate(gun.Name, ammo);
			
			_items.Enqueue(item);
		}
	}
}
