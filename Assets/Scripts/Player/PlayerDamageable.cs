using DamageSystem;
using UnityEngine;

namespace Player {
	public class PlayerDamageable : Damageable {
        public static bool God;

		public override void ApplyDamage(int amount, Quaternion damagerRotation) {
			if(God) return;
			base.ApplyDamage(amount, damagerRotation);
		}
	}
}
