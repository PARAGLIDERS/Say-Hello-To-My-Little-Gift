using EnemySpawning;
using Root;
using TMPro;
using UnityEngine;

namespace Ui.Screens {
	public class UiScreenHud : UiScreen {
		[SerializeField] private TextMeshProUGUI _rounds;
		[SerializeField] private TextMeshProUGUI _waves;
		[SerializeField] private TextMeshProUGUI _enemies;
        		
		public override void Init() {
            // todo: add event bus
		}

		private void UpdateVisuals() {
		}
	}
}