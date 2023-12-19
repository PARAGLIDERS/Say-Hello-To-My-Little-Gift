using Pickupables;
using Root;
using UnityEngine;

namespace GunSystem {
    public class GunPickupable : Pickupbable {
        [SerializeField] private GunType _type;

		protected override void HandlePickup(Collider other) {
			Core.LevelController.GunsController.Pickup(_type);
		}
	}
}
