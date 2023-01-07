using GunSystem.GunComponents;
using UnityEngine;

namespace GunSystem {
	public class AutomaticShotgunGun : Gun {
		[SerializeField] private int _bulletCount = 10;
		[SerializeField] private int _fireRate = 10;

		protected override GunInput GetInput() {
			return new GunInputHold();
		}

		protected override GunShot GetShot() {
			GunShot baseShot = base.GetShot();
			GunShot shotgunShot = new GunShotShotgun(baseShot, _bulletCount);
			GunShot automaticShotgunShot = new GunShotAutomatic(shotgunShot, _fireRate);

			return automaticShotgunShot;
		}
	}
}