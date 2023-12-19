using DamageSystem;
using Pickupables;
using UnityEngine;

namespace Heals {
	public class Heal : Pickupbable {
		[SerializeField] private HealConfig _config;

		protected override void HandlePickup(Collider other) {
			if(other.TryGetComponent(out Damageable damageable)) {
				damageable.Heal(_config.Value);
			}
		}
	}
}
