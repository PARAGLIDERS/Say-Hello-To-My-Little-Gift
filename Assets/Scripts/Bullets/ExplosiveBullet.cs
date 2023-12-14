using DamageSystem;
using UnityEngine;

namespace Bullets {
	public class ExplosiveBullet : Bullet {
		[SerializeField] private Explosion _explosion;

		protected override void Despawn() {
			base.Despawn();
			_explosion.Activate();
		}
	}
}
