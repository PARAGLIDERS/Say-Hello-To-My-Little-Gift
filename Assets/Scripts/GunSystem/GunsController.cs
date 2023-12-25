using Player;
using Root;
using SfxSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GunSystem {
    // i dont like this class to be honest :(
    public class GunsController {
        public event Action<Gun> OnSwitch;
        public event Action<Gun> OnAmmoChange;
        public event Action<Gun, int> OnPickup;

        public Gun Current { get; private set; }
        public readonly List<Gun> Guns;
        private int _currentIndex;
        private bool _afterSwitch;

        public GunsController(GunsControllerConfig config, PlayerController player) {
            Guns = new List<Gun>();

            foreach (GunsConfigItem item in config.Items) {
                Gun gun = GameObject.Instantiate(item.Prefab, player.GunsHolder);
                gun.Type = item.Type;
                gun.NoAmmo += HandleNoAmmo;
                gun.LowAmmo += HandleLowAmmo;
                gun.Deactivate();

                Guns.Add(gun);
            }
        }

        public void Init() {
            Pickup(GunType.Pistol);
        }

        public void Reset() {
            foreach (Gun gun in Guns) {
                if (gun == null) {
                    Debug.LogError("gun is null!");
                    continue;
                }

                gun.Available = false;
                gun.ResetAmmo();
            }

            _currentIndex = 0;
        }

        public void Pickup(GunType type) {
            Gun gun = Get(type);
            int ammo = 0;

			if (!gun.Available) {
                gun.Available = true;
                SwitchTo(gun);
                gun.Pickup(true);
	    		ammo = gun.InitialAmmo;
            } else {
                gun.Pickup(false);
	    		ammo = gun.PickupAmmo;
			}

            Core.EventsBus.Pickup?.Invoke(gun.Name, ammo, gun.Color);
			OnAmmoChange?.Invoke(gun);
        }

        public void Update() {
            if(HandleHotKeys()) return;

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
                _currentIndex = Guns.Count - 1;
            }else if(_currentIndex >= Guns.Count) {
                _currentIndex = 0;
            }

            Gun nextGun = Guns[_currentIndex];

			if (!nextGun.HasAmmo()) {
                Scroll(value);
                return; 
            }

            SwitchTo(nextGun);
        }

        public void SwitchTo(GunType type) {
            Gun gun = Get(type);

            if(!gun.Available) return;
            if(!gun.HasAmmo()) return;

			SwitchTo(gun);
        }

        private void SwitchTo(Gun gun) {
            if (Current != null) {
                Current.Deactivate();
            }

            Current = gun;
            Current.Activate();

            _currentIndex = Guns.IndexOf(Current);

            OnSwitch?.Invoke(Current);
            _afterSwitch = true;
        }

        private Gun Get(GunType type) {
            return Guns.FirstOrDefault(g => g.Type == type);
        }

		private void HandleNoAmmo(Gun gun) {
            Core.SfxController.Play(SfxType.NoAmmo);
            OnAmmoChange?.Invoke(gun);
            gun.Available = false;

            if(_currentIndex == Guns.Count - 1) {
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

        private Dictionary<KeyCode, GunType> _hotKeys = new Dictionary<KeyCode, GunType>() {
            {KeyCode.Alpha0, GunType.Pistol },
            {KeyCode.Alpha1, GunType.Uzi },
            {KeyCode.Alpha2, GunType.Shotgun },
            {KeyCode.Alpha3, GunType.Auto },
            {KeyCode.Alpha4, GunType.DoubleShotgun },
            {KeyCode.Alpha5, GunType.RocketLauncher },
            {KeyCode.Alpha6, GunType.Minigun },
        };

        private bool HandleHotKeys() {
            foreach (var key in _hotKeys.Keys) {
                if(Input.GetKeyDown(key)) {
                    SwitchTo(_hotKeys[key]);
                    return true;
                }
            }

            return false;
        }
	}
}
