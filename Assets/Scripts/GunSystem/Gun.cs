using Root;
using PoolSystem;
using SfxSystem;
using UnityEngine;

namespace GunSystem {
	public class Gun : MonoBehaviour {
        [SerializeField] private GunConfig _config;
		[SerializeField] private Transform _muzzle;

		public GunType Type { get; private set; }
        public bool IsInfinite => _config.IsInfinite;
        public string Name => _config.Name;
        public int Ammo { get; private set; }
        public InputType InputType => _config.InputType;
        public int PickupAmmo => _config.PickupAmmo;
        public int InitialAmmo => _config.InitialAmmo;

        private float _cooldown;
        private bool _dryShotPlayed; // ducktape

        public void Init(GunType type) {
            Type = type;
        }

        public void ResetAmmo() {
            Ammo = _config.InitialAmmo;
        }

        public void Pickup() {
            Ammo += _config.PickupAmmo;
            _dryShotPlayed = false;
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

            for (int i = 0; i < _config.BulletsPerShot; i++) {
                SpawnBullet();
            }

            UpdateCooldown();
            SpendAmmo();

            Core.PoolController.Spawn((PoolType) _config.MuzzleFlashType, _muzzle.position, _muzzle.rotation);
            Core.SfxController.Play((SfxType)_config.ShotSound);
		}       

        private bool CanShoot() {
            return Time.time > _cooldown || _config.InputType == InputType.Click;
		}

        private bool HasAmmo() {
            return _config.IsInfinite || Ammo > 0;
		}

        private void UpdateCooldown() {
			_cooldown = Time.time + 1f / _config.FireRate;
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
			Core.PoolController.Spawn((PoolType) _config.BulletType, _muzzle.position, bulletRotation);
		}

		private Quaternion GetBulletRotation() {
			float spread = Random.Range(-_config.Spread, _config.Spread) * 90f;
			return _muzzle.rotation * Quaternion.AngleAxis(spread, _muzzle.up);
		}
	}
}