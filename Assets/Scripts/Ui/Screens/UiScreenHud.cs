using UnityEngine;
using Ui.Components;

namespace Ui.Screens {
	public class UiScreenHud : UiScreen {
        [SerializeField] private GunHudIconsPanel _gunPanel;
		[SerializeField] private GunHudPickupPanel _gunPickupPanel;
		[SerializeField] private PlayerHealthHudPanel _playerHealthPanel;
		[SerializeField] private EnemySpawnerHudPanel _enemySpawnerPanel;
		[SerializeField] private HudHitVignette _hudHitVignette;

		public override void Init() {
            _gunPanel.Init();
			_gunPickupPanel.Init();
			_playerHealthPanel.Init();
			_enemySpawnerPanel.Init();
			_hudHitVignette.Init();
		}
	}
}