using Components.Hud;
using Root;

namespace Ui.Components {
	public class HudHitVignette : HudVignette {
		protected override void HandleInit() {
			Core.LevelController.Player.OnDamage += Activate;
		}

		protected override void HandleDestroy() {
			Core.LevelController.Player.OnDamage -= Activate;
		}
	}
}
