using System;
using System.Collections.Generic;
using UnityEngine;

namespace GunSystem {
    [CreateAssetMenu(menuName = "Santa/Guns/Guns Controller Config", fileName = "Guns Controller Config")]
    public class GunsControllerConfig : ScriptableObject {
        [SerializeField] private List<GunsConfigItem> _items;
        public List<GunsConfigItem> Items => _items;

        private void OnValidate() {
            foreach (GunsConfigItem item in _items) {
                item.Validate();
            }
        }
    }

    [Serializable]
    public class GunsConfigItem {
        [HideInInspector] public string Name;

        [SerializeField] private GunType _type;
        [SerializeField] private Gun _prefab;

        public GunType Type => _type;
        public Gun Prefab => _prefab;

		public void Validate() {
			Name = Type.ToString();
		}
	}
}
