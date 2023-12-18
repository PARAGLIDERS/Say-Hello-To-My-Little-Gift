using Player;
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
                gun.gameObject.SetActive(false);

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

			if (!AvailableGuns.Contains(gun)) {
                AvailableGuns.Add(gun);
                Sort();
                SwitchTo(gun);
                gun.ResetAmmo();
	    		OnPickup?.Invoke(gun, gun.InitialAmmo);
            } else {
                gun.Pickup();
	    		OnPickup?.Invoke(gun, gun.PickupAmmo);
			}

			OnAmmoChange?.Invoke(gun);
        }

        public void Update() {
            float scroll = Input.mouseScrollDelta.y;
            if (!Mathf.Approximately(scroll, 0f)) {
                Scroll(scroll > 0 ? -1 : 1);
            }

            if (Current == null) return;
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

            SwitchTo(AvailableGuns[_currentIndex]);
        }

        private void SwitchTo(Gun gun) {
            if (Current != null) {
                Current.gameObject.SetActive(false);
            }

            Current = gun;
            Current.gameObject.SetActive(true);

            _currentIndex = AvailableGuns.IndexOf(Current);

            OnSwitch?.Invoke(Current);
        }

        private void Sort() {
            AvailableGuns = AvailableGuns.OrderBy(x => (int)(x.Type)).ToList();
        }
    }
}
