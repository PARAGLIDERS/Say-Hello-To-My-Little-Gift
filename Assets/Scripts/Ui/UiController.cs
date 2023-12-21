using System.Collections.Generic;
using UnityEngine;

namespace Ui {
	public class UiController {
		private readonly Dictionary<UiScreenType, UiScreen> _screens = new();
		private readonly Transform _canvas;
		
		private (UiScreenType type, UiScreen screen) _current;
		private UiScreenType _previous;
		
		public UiController(Transform parent, Canvas canvasPrefab, UiScreenConfig config) {
			_canvas = Object.Instantiate(canvasPrefab, parent).transform;

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
			_current.screen = Object.Instantiate(screen, _canvas);

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

	public enum UiScreenType {
		Main = 0,
		Settings = 1,
		Credits = 2,
		Loading = 3,
		Hud = 4,
		Pause = 5,
		Win = 6,
		Fail = 7,
		ChooseLevel = 8,
		Ending = 9,
		Prefail = 10,
	}
}