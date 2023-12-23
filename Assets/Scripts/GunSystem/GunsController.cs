using Player;
using Root;
using SfxSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GunSystem {
    public class GunsController {
        public event Action<Gun> OnSwitch;
        public event Action<Gun> OnAmmoChange;
        public event Action<Gun, int> OnPickup;

        public List<Gun> AvailableGuns { get; private set; }
        public Gun Current { get; private set; }

        private readonly Dictionary<GunType, Gun> _gunsDictionary;
        private int _currentIndex;
        private bool _afterSwitch;

        public GunsController(GunsControllerConfig config, PlayerController player) {
            AvailableGuns = new List<Gun>();

            _gunsDictionary = new Dictionary<GunType, Gun>();
            foreach (GunsConfigItem item in config.Items) {
                if(_gunsDictionary.ContainsKey(item.Type)) {
                    Debug.LogError($"gun already exists in dictionary: {item.Type}");
                    continue;
                }

                Gun gun = GameObject.Instantiate(item.Prefab, player.GunsHolder);
                gun.Init(item.Type);
                gun.NoAmmo += HandleNoAmmo;
                gun.LowAmmo += HandleLowAmmo;
                gun.Deactivate();

                _gunsDictionary.Add(item.Type, gun);
            }
        }

        public void Init() {
            Pickup(GunType.Pistol);
        }

        public void Reset() {
            AvailableGuns.Clear();

            foreach (KeyValuePair<GunType, Gun> item in _gunsDictionary) {
                if (item.Value == null) {
                    Debug.LogError("gun in dictionary is null!");
                    continue;
                }

                item.Value.ResetAmmo();
            }

            _currentIndex = 0;
        }

        public void Pickup(GunType type) {            
            if(!_gunsDictionary.TryGetValue(type, out Gun gun)){
                Debug.LogError($"weapon does not exist in dictionary: {type}");
                return;
            }

            int ammo;

			if (!AvailableGuns.Contains(gun)) {
                AvailableGuns.Add(gun);
                Sort();
                SwitchTo(gun);
                gun.ResetAmmo();
	    		ammo = gun.InitialAmmo;
            } else {
                gun.Pickup();
	    		ammo = gun.PickupAmmo;
			}

            Core.EventsBus.Pickup?.Invoke(gun.Name, ammo, gun.Color);
			OnAmmoChange?.Invoke(gun);
        }

        public void Update() {
            float scroll = Input.mouseScrollDelta.y;
            if (!Mathf.Approximately(scroll, 0f)) {
                Scroll(scroll > 0 ? -1 : 1);
            }

            if (Current == null) return;
            
            // fixing scroll-shooting :)
            if (_afterSwitch) {
                if (Input.GetMouseButton(0)) {
                    return;
                }
                _afterSwitch = false;
            }

            if (!GetInput(Current.InputType)) return;

            Current.Shoot();
            OnAmmoChange?.Invoke(Current);
        }

        private bool GetInput(InputType inputType) => inputType switch {
            InputType.Click => Input.GetMouseButtonDown(0),
            InputType.Hold => Input.GetMouseButton(0),
            _ => false,
        };

        private void Scroll(int value) {
            _currentIndex += value;

            if(_currentIndex < 0) {
                _currentIndex = AvailableGuns.Count - 1;
            }else if(_currentIndex >= AvailableGuns.Count) {
                _currentIndex = 0;
            }

            Gun nextGun = AvailableGuns[_currentIndex];

			if (!nextGun.HasAmmo()) {
                Scroll(value);
                return; 
            }

            SwitchTo(nextGun);
        }

        private void SwitchTo(Gun gun) {
            if (Current != null) {
                Current.Deactivate();
            }

            Current = gun;
            Current.Activate();

            _currentIndex = AvailableGuns.IndexOf(Current);

            OnSwitch?.Invoke(Current);
            _afterSwitch = true;
        }

        private void Sort() {
            AvailableGuns = AvailableGuns.OrderBy(x => (int)(x.Type)).ToList();
        }

		private void HandleNoAmmo(Gun gun) {
            Core.SfxController.Play(SfxType.NoAmmo);
            OnAmmoChange?.Invoke(gun);

            if(_currentIndex == AvailableGuns.Count - 1) {
                Scroll(-1);
            } else {
                Scroll(1);
            }
		}

        private void HandleLowAmmo(GunType type, float value) {
            SfxType sound = SfxType.Ammo_Low_Bullets;

			switch (type) {
				case GunType.Shotgun:
				case GunType.DoubleShotgun:
					sound = SfxType.Ammo_Low_Shells;
					break;
			}

            Core.SfxController.Play(sound, customVolume: value);
		}
	}
}
