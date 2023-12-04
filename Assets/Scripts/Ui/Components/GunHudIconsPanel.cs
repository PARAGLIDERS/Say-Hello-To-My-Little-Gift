using GunSystem;
using Root;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Ui.Components {
    public class GunHudIconsPanel : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _ammo;
        [SerializeField] private GameObject _infinity;
        [SerializeField] private GunHudIcon _iconPrefab;
        [SerializeField] private RectTransform _container;        
        [SerializeField] private List<GunIconPreset> _presets;

        private Dictionary<GunType, GunIconPreset> _presetsDictionary;
        private Dictionary<GunType, GunHudIcon> _icons;
        private GunHudIcon _current;

        public void Init() {
            _presetsDictionary = new Dictionary<GunType, GunIconPreset>();
            foreach (GunIconPreset item in _presets) {
                if(_presetsDictionary.ContainsKey(item.Type)) {
                    Debug.LogError("gun icon preset already exists in dictionary");
                    continue;
                }

                _presetsDictionary.Add(item.Type, item);
            }

            List<GunType> guns = Core.LevelController.GunsController.AvailableGuns;
            _icons = new Dictionary<GunType, GunHudIcon>();
         
            foreach (GunType gunType in guns) {
                GunHudIcon icon = CreateIcon(gunType);
                _icons.Add(gunType, icon);
            }

            SwitchTo(Core.LevelController.GunsController.Current.type);

            Core.LevelController.GunsController.OnSwitch += SwitchTo;
            Core.LevelController.GunsController.OnShoot += UpdateAmmo;
        }

        private void OnDestroy() {
            Core.LevelController.GunsController.OnSwitch -= SwitchTo;
            Core.LevelController.GunsController.OnShoot -= UpdateAmmo;
        }

        private void SwitchTo(GunType type) {
            if (_current != null) {
                _current.Deselect();
            }

            if(!_icons.TryGetValue(type, out _current)) {
                _current = CreateIcon(type);
                _icons.Add(type, _current);
            }

            _current.Select();
            UpdateAmmo();
        }

        private GunHudIcon CreateIcon(GunType type) {
            if (!_presetsDictionary.TryGetValue(type, out GunIconPreset preset)) {
                Debug.LogError($"no such preset in presets dictionary: {type}");
                return null;
            }

            GunHudIcon icon = Instantiate(_iconPrefab, _container);
            icon.Init(preset.Sprite);
            return icon;
        }

        private void UpdateAmmo() {
            Gun gun = Core.LevelController.GunsController.Current.gun;
            _infinity.SetActive(gun.IsInfinite);
            _ammo.gameObject.SetActive(!gun.IsInfinite);
            _ammo.text = gun.CurrentAmmo.ToString();
        }
    }

    [Serializable]
    public struct GunIconPreset {
        public GunType Type;
        public Sprite Sprite;
    }
}
