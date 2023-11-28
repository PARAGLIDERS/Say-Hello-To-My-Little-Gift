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
        [SerializeField] private BulletType _bulletType = BulletType.Default;
        [SerializeField] private SfxShotType _shotSound = SfxShotType.Pistol;
        [SerializeField] private InputType _inputType = InputType.Hold;
        [SerializeField] private bool _isInfinite;

        private float _cooldown;
        private float _currentAmmo;

        [HideInInspector] public GunType Type;
        public InputType InputType => _inputType;

        public void AddAmmo(int value) {
            if (_isInfinite) return;
            _currentAmmo += value;
        }

        public void SpendAmmo() {
            if (_isInfinite) return;
            _currentAmmo -= _bulletsPerShot;
            if (_currentAmmo < 0) _currentAmmo = 0;
        }

        public bool HasAmmo() {
            return _isInfinite || _currentAmmo > 0;
        }

        public bool CanShoot() {
            return Time.time >= _cooldown;
        }

		public void Shoot() {
            for (int i = 0; i < _bulletsPerShot; i++) {
                SpawnBullet();
            }

            _cooldown = Time.time + 1f / _fireRate;
			
			Core.PoolController.Spawn(PoolType.MuzzleFlash, _muzzle.position, _muzzle.rotation);
            Core.SfxController.Play((SfxType)_shotSound, _muzzle.position);
		}       

		private void SpawnBullet() {
			Quaternion bulletRotation = GetBulletRotation();
			Core.PoolController.Spawn((PoolType) _bulletType, _muzzle.position, bulletRotation);
		}

		private Quaternion GetBulletRotation() {
			float spread = Random.Range(-_spread, _spread) * 90f;
			return _muzzle.rotation * Quaternion.AngleAxis(spread, _muzzle.up);
		}
	}

    public enum BulletType {
        Default = PoolType.Bullet,
        Rocket = PoolType.Rocket,
    }

    public enum SfxShotType {
        Pistol = SfxType.ShotPistol,
        Auto = SfxType.ShotAuto,
        Shotgun = SfxType.ShotShotgun,
    }

    public enum InputType {
        Click = 0,
        Hold = 1,
    }
}