using System.Collections.Generic;
using UnityEngine;

namespace Ui {
	public class UiController {
		private readonly Dictionary<UiScreenType, UiScreen> _screens = new();
		private readonly Transform _canvas;
		
		private UiScreen _current;
		
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

		public void Show(UiScreenType screenType) {
			if (!_screens.TryGetValue(screenType, out UiScreen screen)) {
				Debug.LogError($"screen {screenType} does not exist");
				return;
			}
			
			if(_current != null) Hide(_current);

			_current = Object.Instantiate(screen, _canvas);
			_current.Init();
			_current.Enter();
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

		DeveloperScreen = 100
	}
}