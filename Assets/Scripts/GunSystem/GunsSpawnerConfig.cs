using System;
using System.Collections.Generic;
using UnityEngine;

namespace GunSystem {
    public class GunsSpawnerConfig : ScriptableObject {
        [SerializeField] private float _cooldown;
        [SerializeField] private GunPickupable _pickupablePrefab;
        [SerializeField] private List<GunsSpawnerConfigItem> _items;
        
        public float Cooldown => _cooldown;
        public GunPickupable PickupablePrefab => _pickupablePrefab;
        public List<GunsSpawnerConfigItem> Items => _items;

        // oh no, incapsulation violation :)
        // and code doubling lol
        private void OnValidate() {
            foreach (GunsSpawnerConfigItem item in _items) {
                item.Name = item.Type.ToString();
            }
        }
    }

    [Serializable]
    public class GunsSpawnerConfigItem {
        [HideInInspector] public string Name;

        [SerializeField] private GunType _type;
        [SerializeField] private Mesh _mesh;
        [SerializeField] private Color _color;
        [SerializeField] private int _pickupAmmo;
        [SerializeField] private int _dropChance;

        public GunType Type => _type;
        public Mesh Mesh => _mesh;
        public Color Color => _color;
        public int PickupAmmo => _pickupAmmo;
        public int DropChance => _dropChance;
    }
}
