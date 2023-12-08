using Grid;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Root;

namespace GunSystem {
    public class GunSpawner {
        private readonly Dictionary<GunType, GunsSpawnerConfigItem> _pickupsDictionary;
        private readonly Queue<GunPickupable> _pickups;
        private readonly Transform _container;
        private readonly SpawnerGrid _grid;
        private readonly GunsSpawnerConfig _config;
        
        public GunSpawner(Transform parent, GunsSpawnerConfig config) {
            _config = config;

            _pickupsDictionary = new Dictionary<GunType, GunsSpawnerConfigItem>();
            foreach (GunsSpawnerConfigItem item in _config.Items) {
                if (_pickupsDictionary.ContainsKey(item.Type)) {
                    Debug.LogError($"gun already exists in pickup dictionary: {item.Type}");
                    continue;
                }

                _pickupsDictionary.Add(item.Type, item);
            }

            _container = new GameObject("gun pickups").transform;
            _container.SetParent(parent);

            _pickups = new Queue<GunPickupable>();
            for (int i = 0; i < 100; i++) {
                GunPickupable pickup = CreatePickupable();
                _pickups.Enqueue(pickup);
            }

            _grid = new SpawnerGrid(_config.GridConfig);            
        }

        private IEnumerator _execution;

        public void Start() {
            _grid.CalculatePoints();
            _execution = Execute();
            Core.CoroutineRunner.Run(_execution);
        }

        public void Stop() {
            if (_execution == null) return;
            Core.CoroutineRunner.Stop(_execution);
        }

        private IEnumerator Execute() {
            while (true) {
                yield return new WaitForSeconds(_config.Cooldown);

                if(!Core.LevelController.EnemySpawner.TryGetCurrentGun(out GunType gunType)) {
                    continue;
                }

                if(!_pickupsDictionary.TryGetValue(gunType, out GunsSpawnerConfigItem param)) {
                    Debug.LogError($"no pickup item in dictionary of type: {gunType}");
                    continue;
                }

                GunPickupable pickupable = GetPickupable();
                Vector3 position = _grid.GetPosition();

                pickupable.Activate(param, position);
                _pickups.Enqueue(pickupable);
            }
        }

        private GunPickupable GetPickupable() {
            return _pickups.Peek().IsActive ? CreatePickupable() : _pickups.Dequeue();
        }

        private GunPickupable CreatePickupable() {
            GunPickupable pickup = Object.Instantiate(_config.PickupablePrefab, _container);
            pickup.transform.SetParent(_container);
            return pickup;
        }

        public void Dispose() {
            Object.Destroy(_container);
            _pickups.Clear();
            _pickupsDictionary.Clear();
        }
    }
}
