using GunSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GunSystem {
    public class GunsPickupConfig : ScriptableObject {
        [SerializeField] private List<GunsPickupConfigItem> _items;
        public List<GunsPickupConfigItem> Items => _items;

        // oh no, incapsulation violation :)
        // and code doubling lol
        private void OnValidate() {
            foreach (GunsPickupConfigItem item in _items) {
                item.Name = item.Type.ToString();
            }
        }
    }

    [Serializable]
    public class GunsPickupConfigItem {
        [HideInInspector] public string Name;

        [SerializeField] private GunType _type;
        [SerializeField] private Mesh _mesh;
        [SerializeField] private int _pickupAmmo;
        [SerializeField] private int _dropChance;

        public GunType Type => _type;
        public Mesh Mesh => _mesh;
        public int PickupAmmo => _pickupAmmo;
        public int DropChance => _dropChance;
    }
}
