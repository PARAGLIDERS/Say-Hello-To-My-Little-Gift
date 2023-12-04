using Grid;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Root;

namespace GunSystem {
    public class GunPickupSpawner {
        private readonly Dictionary<GunType, PickupSet> _pickupsDictionary;
        private readonly Queue<GunPickup> _pickups;
        private readonly Transform _container;
        private readonly SpawnerGrid _grid;
        
        private struct PickupSet {
            public int Ammo;
            public int Chance;
        }

        public GunPickupSpawner(Transform parent, GunsConfig _config, SpawnerGridConfig gridConfig) {
            _pickupsDictionary = new Dictionary<GunType, PickupSet>();
            foreach (GunsConfigItem item in _config.Items) {
                if (_pickupsDictionary.ContainsKey(item.Type)) {
                    Debug.LogError($"gun already exists in pickup dictionary: {item.Type}");
                    continue;
                }

                _pickupsDictionary.Add(item.Type, new PickupSet() { 
                    //Ammo = item.PickupAmmo, 
                    //Chance = item.DropChance 
                });
            }

            _container = new GameObject("gun pickups").transform;
            _container.SetParent(parent);

            for (int i = 0; i < 100; i++) {
                GunPickup pickup = new GameObject("gun pickup").AddComponent<GunPickup>();
                pickup.transform.SetParent(_container);
                _pickups.Enqueue(pickup);
            }

            _grid = new SpawnerGrid(gridConfig);            
        }

        private IEnumerator _execution;

        private void Start() {
            _grid.CalculatePoints();
            _execution = Execute();
            Core.CoroutineRunner.Run(_execution);
        }

        private void Stop() {
            if (_execution == null) return;
            Core.CoroutineRunner.Stop(_execution);
        }

        private IEnumerator Execute() {
            while (true) {
                // if cooldown passed
                //      choose weapon to spawn
                //      get position to spawn
                //      get pickup
                //      setup pickup
                //      spawn pickup
                //      update cooldown
                yield return null;
            }
        }


        public void Dispose() {
            Object.Destroy(_container);
            _pickups.Clear();
            _pickupsDictionary.Clear();
        }
    }
}
