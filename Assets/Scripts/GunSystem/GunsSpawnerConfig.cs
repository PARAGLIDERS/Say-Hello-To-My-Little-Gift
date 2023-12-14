using PoolSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GunSystem {
    [CreateAssetMenu(menuName = "Santa/Guns Spawner Config", fileName = "Guns Spawner Config")]
    public class GunsSpawnerConfig : ScriptableObject {
        [SerializeField] private float _cooldown = 3f;
        [SerializeField] private List<GunsSpawnerConfigItem> _items;
        
        public float Cooldown => _cooldown;
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
        [SerializeField] private PoolType _pickupable;

        public GunType Type => _type;
        public PoolType Pickupable => _pickupable;

        public void Validate() {
            Name = _type.ToString();
        }
    }
}
