using Root;
using PoolSystem;
using SfxSystem;
using UnityEngine;

namespace GunSystem {
	public class Gun : MonoBehaviour {
        [SerializeField] private string _name;
		[SerializeField] private Transform _muzzle;
        [SerializeField] [Range(0f, 1f)] private float _spread = 0.1f;
        [SerializeField] [Range(0.1f, 25f)] private float _fireRate = 1f;
        [SerializeField] private int _bulletsPerShot = 1;
        [SerializeField] private BulletType _bulletType = BulletType.Default;
        [SerializeField] private SfxShotType _shotSound = SfxShotType.Pistol;
        [SerializeField] private InputType _inputType = InputType.Hold;
        [SerializeField] private bool _isInfinite;

        public GunType Type { get; private set; }
        public bool IsInfinite => _isInfinite;
        public string Name => _name;
        public int Ammo { get; private set; }
        public InputType InputType => _inputType;
        
        private float _cooldown;
        private bool _dryShotPlayed; // ducktape

        public void Init(GunType type) {
            Type = type;
        }

        public void AddAmmo(int value) {
            if (value < 0) {
                Debug.LogError("trying to add negative ammo!");
                return;
            }

            Ammo += value;
            _dryShotPlayed = false;
        }

        public void ResetAmmo() {
            Ammo = 0;
        }

		public void Shoot() {
            if (!CanShoot()) { 
                return; 
            }

            if (!HasAmmo()) {
                if (!_dryShotPlayed) {
                    Core.SfxController.Play(SfxType.ShotDry);
                    _dryShotPlayed = true;
                }

                return;
            }

            for (int i = 0; i < _bulletsPerShot; i++) {
                SpawnBullet();
            }

            UpdateCooldown();
            SpendAmmo();

            Core.PoolController.Spawn(PoolType.MuzzleFlash, _muzzle.position, _muzzle.rotation);
            Core.SfxController.Play((SfxType)_shotSound);
		}       

        private bool CanShoot() {
            return Time.time > _cooldown || _inputType == InputType.Click;
		}

        private bool HasAmmo() {
            return _isInfinite || Ammo > 0;
		}

        private void UpdateCooldown() {
			_cooldown = Time.time + 1f / _fireRate;
		}

        private void SpendAmmo() {
            if (IsInfinite) {
                return;
            }

			Ammo--;

            if (Ammo < 0) {
                Debug.LogError("ammo dropped below zero!");
                Ammo = 0;
            }
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
        Uzi = SfxType.ShotUzi,
    }

    public enum InputType {
        Click = 0,
        Hold = 1,
    }
}