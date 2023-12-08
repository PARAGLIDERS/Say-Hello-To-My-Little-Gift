using Player;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GunSystem {
    public class GunsController {
        public event Action<GunType, IGun> OnSwitch;
        public event Action<GunType, IGun> OnAmmoChange;
        public event Action<IGun, int> OnPickup;

        public readonly List<GunType> AvailableGuns;
        public (GunType type, Gun gun) Current { get; private set; }

        private readonly Dictionary<GunType, Gun> _gunsDictionary;
        private int _currentIndex;

        public GunsController(GunsConfig config, PlayerController player) {
            AvailableGuns = new List<GunType>();

            _gunsDictionary = new Dictionary<GunType, Gun>();
            foreach (GunsConfigItem item in config.Items) {
                if(_gunsDictionary.ContainsKey(item.Type)) {
                    Debug.LogError($"gun already exists in dictionary: {item.Type}");
                    continue;
                }

                Gun gun = GameObject.Instantiate(item.Prefab, player.GunsHolder);
                gun.gameObject.SetActive(false);

                _gunsDictionary.Add(item.Type, gun);
            }
        }

        public void Init() {
            Pickup(GunType.Pistol, 0);
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

        public void Pickup(GunType type, int ammo) {
            if(!_gunsDictionary.TryGetValue(type, out Gun gun)){
                Debug.LogError($"weapon does not exist in dictionary: {type}");
                return;
            }

            gun.AddAmmo(ammo);

			if (!AvailableGuns.Contains(type)) {
                AvailableGuns.Add(type);
                SwitchTo(type);
            } else {
				OnAmmoChange?.Invoke(type, gun);
			}

			OnPickup?.Invoke(gun, ammo);
        }

        public void Update() {
            float scroll = Input.mouseScrollDelta.y;
            if (!Mathf.Approximately(scroll, 0f)) {
                Scroll(scroll > 0 ? -1 : 1);
            }

            if (Current.gun == null) return;
            if (!GetInput(Current.gun.InputType)) return;

            Current.gun.Shoot();
            OnAmmoChange?.Invoke(Current.type, Current.gun);
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

            SwitchTo(AvailableGuns[_currentIndex]);
        }

        private void SwitchTo(GunType type) {
            if (!_gunsDictionary.TryGetValue(type, out Gun gun)) {
                Debug.LogError($"gun does not exist in dictionary: {type}");
                return;
            }

            if (Current.gun != null) {
                Current.gun.gameObject.SetActive(false);
            }

            Current = new (type, gun);
            Current.gun.gameObject.SetActive(true);

            OnSwitch?.Invoke(type, gun);
        }
    }
}
