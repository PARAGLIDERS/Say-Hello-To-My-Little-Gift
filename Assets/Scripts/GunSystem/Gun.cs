using CameraControl;
using Root;
using PoolSystem;
using SfxSystem;
using UnityEngine;

namespace GunSystem {
	public class Gun : MonoBehaviour {
		[SerializeField] private Transform _muzzle;
        [SerializeField] [Range(0f, 1f)] private float _spread = 0.1f;
        [SerializeField] [Range(0.1f, 25f)] private float _fireRate = 1f;
        [SerializeField] private int _bulletsPerShot = 1;
        [SerializeField] private BulletType _bulletType;
        [SerializeField] private SfxType _shotSound;
        [SerializeField] private bool _isInfinite;

        private float _cooldown;
        private float _currentAmmo;

        public GunType Type;

        public void AddAmmo(int value) {
            if (_isInfinite) return;
            _currentAmmo += value;
        }

		public void Shoot() {
            if (_currentAmmo <= 0 && !_isInfinite) return;
            if (Time.time < _cooldown) return;

            for (int i = 0; i < _bulletsPerShot; i++) {
                SpawnBullet();
            }

            _currentAmmo -= _bulletsPerShot;
            if(_currentAmmo < 0) _currentAmmo = 0;

            _cooldown = Time.time + 1f / _fireRate;
			
            CameraShaker.Shake();
			Core.PoolController.Spawn(PoolType.MuzzleFlash, _muzzle.position, _muzzle.rotation);
		}       

		private void SpawnBullet() {
			Quaternion bulletRotation = GetBulletRotation();
			Core.PoolController.Spawn((PoolType) _bulletType, _muzzle.position, bulletRotation);
		}

		private Quaternion GetBulletRotation() {
			float spread = Random.Range(-_spread, _spread);
			return _muzzle.rotation * Quaternion.AngleAxis(spread, _muzzle.up);
		}
	}

    public enum BulletType {
        Default = PoolType.Bullet,
        Rocket = PoolType.Rocket,
    }
}