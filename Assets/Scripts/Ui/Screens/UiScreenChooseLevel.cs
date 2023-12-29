using DG.Tweening;
using GameStateMachine;
using Level;
using Root;
using System.Collections.Generic;
using TMPro;
using Ui.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens {
	public class UiScreenChooseLevel : UiScreenBlurredBack {
		[SerializeField] private Button _backButton;
		[SerializeField] private Button _clearData;

		[SerializeField] private RectTransform _itemsContainer;
		[SerializeField] private ChooseLevelItem _itemPrefab;

		[SerializeField] private TextMeshProUGUI _title;

		private List<ChooseLevelItem> _items = new List<ChooseLevelItem>();

		public override void Init() {
			_backButton.onClick.AddListener(HandleBackButton);

			List<LevelsConfigItem> levels = Core.LevelController.GetLevels();
			int currentLevel = Core.DataController.Data.LevelData.CurrentLevel;

			_clearData.gameObject.SetActive(currentLevel > 0);
			_clearData.onClick.AddListener(HandleClearDataButton);

			for (int i = 0; i < levels.Count; i++) {
				ChooseLevelItem item = Instantiate(_itemPrefab, _itemsContainer);
				bool available = currentLevel >= i;
				int level = i;
				item.Init(levels[i], available, () => HandlePlay(level));
				_items.Add(item);
			}
		}

		private void HandleBackButton() {
			Core.UiController.Show(UiScreenType.Main);
		}

		private void HandlePlay(int level) {
			Core.LevelController.ChooseLevel(level);
			Core.StateController.SetState(StateType.LoadLevel);
		}

		private void HandleClearDataButton() {
			Core.DataController.Data.DropLevelData();
			Core.DataController.Save();
			Core.UiController.Show(UiScreenType.ChooseLevel);
		}

		protected override void PlayEnterAnim() {
			base.PlayEnterAnim();

			Sequence seq = DOTween.Sequence();

			_title.alpha = 0f;
			seq.Insert(Constants.UiAnimDelay, _title.DOFade(1f, Constants.UiAnimDuration));

			for (int i = 0; i < _items.Count; i++) {
				seq.Insert(Constants.UiAnimDelay + Constants.UiAnimInterval * i, _items[i].GetAnim());
			}

			_backButton.transform.localScale = Vector3.zero;
			_clearData.transform.localScale = Vector3.zero;

			seq.Append(_backButton.transform.DOScale(1f, Constants.UiAnimDuration).SetEase(Ease.OutBack));
			seq.Append(_clearData.transform.DOScale(1f, Constants.UiAnimDuration).SetEase(Ease.OutBack));
		}
	}
}
