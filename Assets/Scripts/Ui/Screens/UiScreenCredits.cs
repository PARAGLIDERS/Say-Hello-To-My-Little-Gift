using Root;
using Ui.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens {
	public class UiScreenCredits : UiScreenBlurredBack {
		[SerializeField] private Button _back;
		//[SerializeField] private CreditsConfig _config;
		//[SerializeField] private CreditCard _cardPrefab;
		//[SerializeField] private RectTransform _cardsContainer;

		public override void Init() {
			_back.onClick.AddListener(HandleBack);

			//foreach (CreditsConfigItem item in _config.Items) {
			//	CreditCard card = Instantiate(_cardPrefab, _cardsContainer);
			//	card.Init(item);
			//}
		}

		private void HandleBack() {
			Core.UiController.Show(UiScreenType.Main);
		}
	}
}
