using GunSystem.GunComponents;
using UnityEngine;

namespace GunSystem {
	public class ShotgunGun : Gun {
		[SerializeField] private int _bulletCount = 10;

		protected override GunShot GetShot() {
			GunShot baseShot = base.GetShot();
			GunShot shotgunShot = new GunShotShotgun(baseShot, _bulletCount);

			return shotgunShot;
		}
	}
}