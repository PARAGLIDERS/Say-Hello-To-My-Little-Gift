using GunSystem;
using Root;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ui.Components {
    public class GunHudIconsPanel : MonoBehaviour {
        [SerializeField] private GunHudIcon _iconPrefab;
        [SerializeField] private RectTransform _container;        
        [SerializeField] private List<GunIconPreset> _presets;

        private Dictionary<GunType, GunIconPreset> _presetsDictionary;
        private Dictionary<GunType, GunHudIcon> _icons;
        private GunHudIcon _current;

        public void Init() {
            InitPresets();
            InitIcons();
            SubscribeToEvents();
            SetInitialIcon();
        }

		private void OnDestroy() {
			UnsubscriveFromEvents();
		}

		private void InitPresets() {
			_presetsDictionary = new Dictionary<GunType, GunIconPreset>();
			
            foreach (GunIconPreset item in _presets) {
				if (_presetsDictionary.ContainsKey(item.Type)) {
					Debug.LogError("gun icon preset already exists in dictionary");
					continue;
				}

				_presetsDictionary.Add(item.Type, item);
			}
		}

        private void InitIcons() {
			List<Gun> guns = Core.LevelController.GunsController.AvailableGuns;
			_icons = new Dictionary<GunType, GunHudIcon>();

			foreach (Gun gun in guns) {
				GunHudIcon icon = CreateIcon(gun.Type);
				_icons.Add(gun.Type, icon);
                icon.UpdateAmmo(gun);
			}
		}

        private void SubscribeToEvents() {
			Core.LevelController.GunsController.OnSwitch += SwitchTo;
			Core.LevelController.GunsController.OnAmmoChange += UpdateIcon;
		}

        private void UnsubscriveFromEvents() {
			Core.LevelController.GunsController.OnSwitch -= SwitchTo;
			Core.LevelController.GunsController.OnAmmoChange -= UpdateIcon;
		}

		private void SetInitialIcon() {
			GunsController GunsController = Core.LevelController.GunsController;
			SwitchTo(GunsController.Current);
		}

		private void SwitchTo(Gun gun) {
            if (_current != null) {
                _current.Deselect();
            }

            if(!_icons.TryGetValue(gun.Type, out _current)) {
                _current = CreateIcon(gun.Type);
                _icons.Add(gun.Type, _current);
            }

            _current.Select();
            _current.UpdateAmmo(gun);
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

        private void UpdateIcon(Gun gun) {
            if(!_icons.TryGetValue(gun.Type, out GunHudIcon icon)) {
                Debug.LogError($"icon does not exist in dictionary: {gun.Type}");
                return;
            }

            icon.UpdateAmmo(gun);
        }
    }

    [Serializable]
    public struct GunIconPreset {
        public GunType Type;
        public Sprite Sprite;
    }
}
