using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace GunSystem {
    [CreateAssetMenu(menuName = "Santa/Guns Config", fileName = "Guns Config")]
    public class GunsConfig : ScriptableObject {
        [SerializeField] private List<GunsConfigItem> _items;
        public List<GunsConfigItem> Items => _items;

        // oh no, incapsulation violation :)
        // and code doubling lol
        private void OnValidate() {
            foreach (GunsConfigItem item in _items) {
                item.Name = item.Type.ToString();
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
    }
}