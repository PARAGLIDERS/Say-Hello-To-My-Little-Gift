using CameraControl;
using GunSystem.GunComponents;
using Misc.Root;
using PoolSystem;
using SfxSystem;
using UnityEngine;

namespace GunSystem {
	public abstract class Gun : MonoBehaviour {
		[SerializeField] private Transform _muzzle;

		private GunAccuracy _accuracy;
		private GunAccuracy Accuracy => _accuracy ??= GetAccuracy();

		protected virtual GunAccuracy GetAccuracy() => new GunAccuracyDefault();

		private GunInput _input;
		private GunInput Input => _input ??= GetInput();

		protected virtual GunInput GetInput() => new GunInputClick();

		private GunShot _shot;
		private GunShot Shot => _shot ??= GetShot();

		protected virtual GunShot GetShot() => new GunShotDefault(SpawnBullet);
		
		private void Update() {
			if (Input.Handled && Shot.Available) {
				Shot.Execute();
				CameraShaker.Shake();
				Core.PoolController.Spawn(PoolType.MuzzleFlash, _muzzle.position, _muzzle.rotation);
			}
		}
	
		private void SpawnBullet() {
			Quaternion bulletRotation = GetBulletRotation();
			Core.PoolController.Spawn(PoolType.Bullet, _muzzle.position, bulletRotation);
		}

		private Quaternion GetBulletRotation() {
			float spread = Random.Range(-Accuracy.Value, Accuracy.Value);
			return _muzzle.rotation * Quaternion.AngleAxis(spread, _muzzle.up);
		}
	}
}