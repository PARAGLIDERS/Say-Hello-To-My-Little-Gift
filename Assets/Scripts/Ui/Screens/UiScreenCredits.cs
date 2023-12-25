using Root;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens {
	public class UiScreenCredits : UiScreenBlurredBack {
		[SerializeField] private Button _close;
		[SerializeField] private Button _next;
		[SerializeField] private GameObject _team;
		[SerializeField] private GameObject _assets;

		public override void Init() {
			_assets.SetActive(false);
			_team.SetActive(true);

			_close.onClick.AddListener(HandleClose);
			_next.onClick.AddListener(HandleNext);
		}

		private void HandleNext() {
			_assets.SetActive(true);
			_team.SetActive(false);
		}

		private void HandleClose() {
			Core.UiController.Show(UiScreenType.Main);
		}
	}
}
