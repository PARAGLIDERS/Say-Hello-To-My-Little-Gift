using UnityEngine;

namespace Enemies {
	public abstract class EnemyRabbitBase : Enemy {
		[SerializeField] private Swing _swing;

		protected override void Attack() {
			_swing.Activate();
		}
	}
}
