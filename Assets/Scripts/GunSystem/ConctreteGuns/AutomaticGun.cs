using GunSystem.GunComponents;
using UnityEngine;

namespace GunSystem {
	public class AutomaticGun : Gun {
		[SerializeField] private int _fireRate = 10;

		protected override GunInput GetInput() {
			return new GunInputHold();
		}

		protected override GunShot GetShot() {
			GunShot baseShot = base.GetShot();
			GunShot automaticShot = new GunShotAutomatic(baseShot, _fireRate);

			return automaticShot;
		}
	}
}