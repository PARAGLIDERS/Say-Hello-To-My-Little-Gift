using Components.Hud;
using Root;

namespace Ui.Components.Hud {
	public class HudHealVignette : HudVignette {
		protected override void HandleInit() {
			Core.LevelController.Player.OnHeal += Activate;
		}

		protected override void HandleDestroy() {
			Core.LevelController.Player.OnHeal -= Activate;
		}
	}
}
