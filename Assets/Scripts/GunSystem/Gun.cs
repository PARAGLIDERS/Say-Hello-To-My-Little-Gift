using Root;
using PoolSystem;
using SfxSystem;
using UnityEngine;
using System;

namespace GunSystem {
	public class Gun : MonoBehaviour {
        [SerializeField] protected GunConfig _config;
		[SerializeField] private Transform _muzzle;

        public static bool Endless;

        public event Action<Gun> NoAmmo;
        public event Action<GunType, float> LowAmmo;

		public GunType Type { get; set; }
        public bool Available { get; set; }

        public Color Color => _config.Color;
        public bool IsInfinite => Endless || _config.IsInfinite;
        public string Name => _config.Name;
        public int Ammo { get; private set; }
        public InputType InputType => _config.InputType;
        public int PickupAmmo => _config.PickupAmmo;
        public int InitialAmmo => _config.InitialAmmo;
        public bool IsLowAmmo => Ammo <= _config.LowAmmo;

        private float _cooldown;
        private bool _dryShotPlayed; // ducktape

        public void Activate() {
            gameObject.SetActive(true);
        }

        public void Deactivate() {
			gameObject.SetActive(false);
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
            OnShoot();

            Core.PoolController.Spawn((PoolType) _config.MuzzleFlashType, _muzzle.position, _muzzle.rotation);
            Core.SfxController.Play((SfxType)_config.ShotSound);
		}       

        private bool CanShoot() {
            return Time.time > _cooldown || _config.InputType == InputType.Click;
		}

        public bool HasAmmo() {
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

            if (IsLowAmmo) {
                LowAmmo?.Invoke(Type, 1.5f - (float)Ammo / _config.LowAmmo);
            }

            if (Ammo <= 0) {
                Ammo = 0;
                NoAmmo?.Invoke(this);
            }
		}

		private void SpawnBullet() {
			Quaternion bulletRotation = GetBulletRotation();
			Core.PoolController.Spawn((PoolType) _config.BulletType, _muzzle.position, bulletRotation);
		}

		private Quaternion GetBulletRotation() {
			float spread = UnityEngine.Random.Range(-_config.Spread, _config.Spread) * 90f;
			return _muzzle.rotation * Quaternion.AngleAxis(spread, _muzzle.up);
		}

        protected virtual void OnShoot() { }
	}
}