using System.Collections.Generic;
using UnityEngine;

namespace Ui {
	public class UiService {
		private readonly Dictionary<UiScreenType, UiScreen> _screens = new();
		public readonly Canvas Canvas;
		
		private (UiScreenType type, UiScreen screen) _current;
		private UiScreenType _previous;
		
		public UiService(Transform parent, Canvas canvasPrefab, UiScreenConfig config) {
			Canvas = Object.Instantiate(canvasPrefab, parent);

			foreach (UiScreenConfigItem item in config.Items) {
                if (_screens.ContainsKey(item.Type)) {
                    Debug.LogError($"screen already exists in dictionary: {item.Type}");
                    continue;
                }

				_screens.Add(item.Type, item.Prefab);
			}
		}

		public void Show(UiScreenType screenType, bool rememberCurrent = false) {
			if (!_screens.TryGetValue(screenType, out UiScreen screen)) {
				Debug.LogError($"screen {screenType} does not exist");
				return;
			}

			if (rememberCurrent) _previous = _current.type;
			if (_current.screen != null) Hide(_current.screen);

			_current.type = screenType;
			_current.screen = Object.Instantiate(screen, Canvas.transform);

			_current.screen.Init();
			_current.screen.Enter();
		}

		public void HideCurrent() {
			if (_current.screen != null ) Hide(_current.screen);
			Show(_previous);
		}

		private void Hide(UiScreen screen) {
			screen.Exit();
			Object.Destroy(screen.gameObject);
		}
	}
}