using Grid;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Root;
using PoolSystem;
using UnityEngine.UIElements;

namespace GunSystem {
    public class GunSpawner {
        private readonly Dictionary<GunType, GunsSpawnerConfigItem> _pickupsDictionary;
        private readonly GunsSpawnerConfig _config;

		private IEnumerator _execution;
		private SpawnerGrid _grid;
        
        public GunSpawner(GunsSpawnerConfig config) {
            _config = config;

            _pickupsDictionary = new Dictionary<GunType, GunsSpawnerConfigItem>();
            foreach (GunsSpawnerConfigItem item in _config.Items) {
                if (_pickupsDictionary.ContainsKey(item.Type)) {
                    Debug.LogError($"gun already exists in pickup dictionary: {item.Type}");
                    continue;
                }

                _pickupsDictionary.Add(item.Type, item);
            }        
        }


        public void Start(SpawnerGridConfig gridConfig) {
            _grid = new SpawnerGrid(gridConfig);
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
					Debug.LogError($"can't get gun from spawner: {gunType}");
					continue;
                }

                if(!_pickupsDictionary.TryGetValue(gunType, out GunsSpawnerConfigItem param)) {
                    Debug.LogError($"no pickup item in dictionary of type: {gunType}");
                    continue;
                }
                
                Vector3 position = _grid.GetPosition();
                Quaternion rotation = Quaternion.identity;

                Core.PoolController.Spawn(param.Pickupable, position, rotation);
				Core.PoolController.Spawn(PoolType.VFX_GunSpawnVfx, position, rotation);

			}
		}
    }
}
