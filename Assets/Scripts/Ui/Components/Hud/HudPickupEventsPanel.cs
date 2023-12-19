using GunSystem;
using Root;
using System.Collections.Generic;
using UnityEngine;

namespace Ui.Components.Hud {
	public class HudPickupEventsPanel : MonoBehaviour {
		[SerializeField] private HudPickupEventPanelItem _itemPrefab;
		[SerializeField] private RectTransform _itemsContainer;
		[SerializeField] private int _maxItemsCount;

		private Queue<HudPickupEventPanelItem> _items;

		public void Init() {
			InitItems();
			SubscribeToEvents();
		}

		private void OnDestroy() {
			UnsubscribeFromEvents();
		}

		public void InitItems() {
			_items = new Queue<HudPickupEventPanelItem>();

			for (int i = 0; i < _maxItemsCount; i++) {
				HudPickupEventPanelItem item = Instantiate(_itemPrefab, _itemsContainer);
				item.Init();
				_items.Enqueue(item);
			}
		}

		private void SubscribeToEvents() {
			Core.EventsBus.Pickup += HandlePickup;
		}

		private void UnsubscribeFromEvents() {
			Core.EventsBus.Pickup -= HandlePickup;
		}

		private void HandlePickup(string pickupName, int count, Color color) {
			HudPickupEventPanelItem item = _items.Dequeue();

			item.Deactivate();
			item.Activate(pickupName, count, color);

			_items.Enqueue(item);
		}
	}
}
