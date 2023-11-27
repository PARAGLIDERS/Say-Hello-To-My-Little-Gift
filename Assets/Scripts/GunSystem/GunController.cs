using System;
using System.Collections.Generic;
using UnityEngine;

namespace GunSystem {
    public class GunController {
        public event Action<GunType> OnSwitch;

        private readonly Dictionary<GunType, Gun> _guns;
        private readonly List<GunType> _availableGuns;
        
        private Gun _current;

        public GunController(GunsConfig config, Transform gunsHolder) {
            _availableGuns = new List<GunType>();

            _guns = new Dictionary<GunType, Gun>();
            foreach (GunsConfigItem item in config.Items) {
                if(_guns.ContainsKey(item.Type)) {
                    Debug.LogError($"gun already exists: {item.Type}");
                    continue;
                }

                Gun gun = GameObject.Instantiate(item.Prefab, gunsHolder);
                gun.Type = item.Type;
                gun.gameObject.SetActive(false);
                _guns.Add(item.Type, gun);
            }
        }

        public void Init() {
            _availableGuns.Clear();
            _availableGuns.Add(GunType.Pistol);

            SwitchTo(GunType.Pistol);
        }

        public void Pickup(GunType type, int ammo) {
            if(!_guns.TryGetValue(type, out Gun gun)){
                Debug.LogError($"weapon does not exist in dictionary: {type}");
                return;
            }

            gun.AddAmmo(ammo);

            if(!_availableGuns.Contains(type)) {
                _availableGuns.Add(type);
                SwitchTo(type);
            }
        }

        public void SwitchTo(GunType type) {
            if(!_guns.TryGetValue(type, out Gun gun)) {
                Debug.LogError($"gun does not exist in dictionary: {type}");
                return;
            }

            _current?.gameObject.SetActive(false);
            _current = gun;
            _current?.gameObject.SetActive(true);

            OnSwitch?.Invoke(type);
        }

        public void Update() {
            if (Input.GetMouseButton(0)) {
                _current?.Shoot();
            }

            float scroll = Input.mouseScrollDelta.y;
            if (!Mathf.Approximately(scroll, 0f)) {
                Scroll(scroll > 0 ? 1 : -1);
            }
        }

        private void Scroll(int value) {
            int currentIndex = _current != null ? _availableGuns.IndexOf(_current.Type) : 0;
            int nextIndex = currentIndex + value;

            if(nextIndex < 0) { 
                nextIndex = _availableGuns.Count - 1;
            }else if(nextIndex >= _availableGuns.Count) {
                nextIndex = 0;
            }

            SwitchTo(_availableGuns[nextIndex]);
        }
    }
}
