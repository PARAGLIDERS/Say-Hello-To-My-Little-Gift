using Dash;
using Root;
using System.Collections.Generic;
using UnityEngine;

namespace Ui.Components.Hud {
	public class HudDashPanel : MonoBehaviour {
		[SerializeField] private RectTransform _container;
		[SerializeField] private HudDashDot _dotPrefab;
		[SerializeField] private PlayerDashConfig _config;

		private List<HudDashDot> _dots = new List<HudDashDot>();
		private	int _count => Core.LevelController.Player.DashCount;
		
		public void Init() {
			for (int i = 0; i < _config.Count; i++) {
				var dot = Instantiate(_dotPrefab, _container);
				_dots.Add(dot);
			}

			Core.LevelController.Player.DashChanged += HandleChange;
		}

		private void OnDestroy() {
			Core.LevelController.Player.DashChanged -= HandleChange;
		}

		private void HandleChange() {
			for (int i = 0;i < _dots.Count;i++) {
				_dots[i].SetActive(i < _count);
			}
		}
	}
}
