using UnityEngine;
using Ui.Components;
using Ui.Components.Hud;

namespace Ui.Screens {
	public class UiScreenHud : UiScreen {
		[SerializeField] private GameObject _container;
        [SerializeField] private GunHudIconsPanel _gunPanel;
		[SerializeField] private HudPickupEventsPanel _pickupEventsPanel;
		[SerializeField] private PlayerHealthHudPanel _playerHealthPanel;
		[SerializeField] private EnemySpawnerHudPanel _enemySpawnerPanel;
		[SerializeField] private HudHitVignette _hudHitVignette;
		[SerializeField] private HudHealVignette _hudHealVignette;
		[SerializeField] private HudLowAmmoPanel _lowAmmoPanel;
		[SerializeField] private HudDashPanel _dashPanel;

		public static bool Active = true;

		public override void Init() {
			_container.SetActive(Active);
            _gunPanel.Init();
			_pickupEventsPanel.Init();
			_playerHealthPanel.Init();
			_enemySpawnerPanel.Init();
			_hudHitVignette.Init();
			_hudHealVignette.Init();
			_lowAmmoPanel.Init();
			_dashPanel.Init();
		}
	}
}