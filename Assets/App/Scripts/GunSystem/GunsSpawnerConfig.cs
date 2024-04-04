using Pooling;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GunSystem {
    [CreateAssetMenu(menuName = "Santa/Guns/Guns Spawner Config", fileName = "Guns Spawner Config")]
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
        [SerializeField] private Pooling.ObjectType _pickupable;
        [SerializeField] private Pooling.ObjectType _spawnEffect;

        public GunType Type => _type;
        public Pooling.ObjectType Pickupable => _pickupable;
        public Pooling.ObjectType SpawnEffect => _spawnEffect;

        public void Validate() {
            Name = _type.ToString();
        }
    }
}
