using DamageSystem;
using Pickupables;
using Root;
using UnityEngine;

namespace Heals {
	public class Heal : Pickupbable {
		[SerializeField] private HealConfig _config;

		// triggers only player
		protected override void HandlePickup(Collider other) {
			if(other.TryGetComponent(out Damageable damageable)) {
				damageable.Heal(_config.Value);
				Core.EventsBus.Pickup?.Invoke(_config.Name, _config.Value, _config.Color); // ducktape
			}
		}
	}
}
