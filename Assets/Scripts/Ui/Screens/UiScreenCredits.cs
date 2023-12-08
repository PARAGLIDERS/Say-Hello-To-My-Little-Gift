using Root;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens {
	public class UiScreenCredits : UiScreen {
		[SerializeField] private Button _back;

		public override void Init() {
			_back.onClick.AddListener(HandleBack);
		}

		private void HandleBack() {
			Core.UiController.Show(UiScreenType.Main);
		}
	}
}
