using GameStateMachine;
using Level;
using Root;
using System.Collections.Generic;
using Ui.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens {
	public class UiScreenChooseLevel : UiScreenBlurredBack {
		[SerializeField] private Button _backButton;
		[SerializeField] private RectTransform _itemsContainer;
		[SerializeField] private ChooseLevelItem _itemPrefab;

		public override void Init() {
			_backButton.onClick.AddListener(HandleBackButton);

			List<LevelsConfigItem> levels = Core.LevelController.GetLevels();
			int currentLevel = Core.DataController.Data.LevelData.CurrentLevel;
			Debug.LogError(currentLevel);

			for (int i = 0; i < levels.Count; i++) {
				ChooseLevelItem item = Instantiate(_itemPrefab, _itemsContainer);
				bool available = currentLevel >= i;
				int level = i;
				item.Init(levels[i], available, () => HandlePlay(level));
			}
		}

		private void HandleBackButton() {
			Core.UiController.Show(UiScreenType.Main);
		}

		private void HandlePlay(int level) {
			Core.LevelController.ChooseLevel(level);
			Core.StateController.SetState(StateType.LoadLevel);
		}
	}
}
