using Grid;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GunSystem {
    [CreateAssetMenu(menuName = "Santa/Guns Spawner Config", fileName = "Guns Spawner Config")]
    public class GunsSpawnerConfig : ScriptableObject {
        [SerializeField] private SpawnerGridConfig _gridConfig;
        [SerializeField] private float _cooldown;
        [SerializeField] private GunPickupable _pickupablePrefab;
        [SerializeField] private List<GunsSpawnerConfigItem> _items;
        
        public SpawnerGridConfig GridConfig => _gridConfig;
        public float Cooldown => _cooldown;
        public GunPickupable PickupablePrefab => _pickupablePrefab;
        public List<GunsSpawnerConfigItem> Items => _items;

        private void OnValidate() {
            foreach (GunsSpawnerConfigItem item in _items) {
                item.Validate();
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

        public void Validate() {
            Name = _type.ToString();
        }
    }
}
