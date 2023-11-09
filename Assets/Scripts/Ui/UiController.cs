using System.Collections.Generic;
using Misc.Root;
using UnityEngine;

namespace Ui {
	public class UiController {
		private readonly Dictionary<UiScreenType, UiScreen> _screens = new();
		private readonly Transform _canvas;
		
		private UiScreen _current;
		
		public UiController(Transform parent) {
			_canvas = Object.Instantiate(Core.Resources.CanvasPrefab, parent).transform;

			foreach (UiScreenConfig screenConfig in Core.Resources.ScreenConfigs) {
				_screens.TryAdd(screenConfig.Type, screenConfig.Prefab);
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
		}

		private void Hide(UiScreen screen) {
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
	}
}