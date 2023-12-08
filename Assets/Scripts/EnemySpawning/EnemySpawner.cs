using System;
using System.Collections;
using Root;
using PoolSystem;
using UnityEngine;
using Grid;
using RandomSystem;
using GunSystem;

namespace EnemySpawning {
	public class EnemySpawner {
		public Action OnChange;

		private readonly EnemySpawnerConfig _config;
		private readonly SpawnerGrid _grid;

		private Action _onAllEnemiesKilled;

		public int CurrentWaveEnemiesCountMax { get; private set; }
		
		private int _currentWaveEnemyCount;
		public int CurrentEnemyCount {
			get => _currentWaveEnemyCount;
			set {
				_currentWaveEnemyCount = value;
				OnChange?.Invoke();
			}
		}

		private int _currentRound;
		public int CurrentRound {
			get => _currentRound;
			set {
				_currentRound = value;
				OnChange?.Invoke();
			}
		}

		private int _currentWave;
		public int CurrentWave {
			get => _currentWave;
			set {
				_currentWave = value;
				OnChange?.Invoke();
			}
		}

        private IEnumerator _execution;

		public EnemySpawner(EnemySpawnerConfig config) {
			_config = config;
			_grid = new SpawnerGrid(config.GridConfig);
		}

		private Randomizer<EnemySpawnRoundGun> _gunRandomizer;
		public bool TryGetCurrentGun(out GunType type) {
			if( _gunRandomizer == null ) {
				type = GunType.Pistol;
				return false;
			}

			type = _gunRandomizer.GetItem().Type;
			return true;
		}

        public void Reset() {
            CurrentRound = 0;
            CurrentWave = 0;
            CurrentEnemyCount = 0;
        }

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
            EnemySpawnRound round;
            EnemySpawnWave wave;

			while (CurrentRound < _config.Rounds.Count) {
                round = _config.Rounds[CurrentRound];
				_gunRandomizer = new Randomizer<EnemySpawnRoundGun>(round.Guns);

				yield return new WaitForSeconds(round.Delay);

                while (CurrentWave < round.Waves.Count) {
                    wave = round.Waves[CurrentWave];
                    Randomizer<EnemySpawnWaveUnit> randomizer = new Randomizer<EnemySpawnWaveUnit>(wave.Units);

                    yield return new WaitForSeconds(wave.Delay);

                    CurrentEnemyCount += wave.EnemyCount;

					for (int i = 0; i < wave.EnemyCount; i++) {
                        Spawn(randomizer.GetItem().Type);
						yield return new WaitForSeconds(wave.Period);
					}

                    float cooldown = _config.WaveCooldown;

                    while (cooldown > 0) {
                        if (CurrentEnemyCount == 0) {
                            break;
                        }

                        cooldown -= Time.deltaTime;
                        yield return null;
                    }

                    CurrentWave++;
				}
				
				CurrentRound++;
                CurrentWave = 0;
			}

			yield return new WaitUntil(() => CurrentEnemyCount <= 0);
                
            _onAllEnemiesKilled?.Invoke();
            _onAllEnemiesKilled = null;
		}
                
        private void Spawn(EnemySpawnerUnitType type) {
            Vector3 position = _grid.GetPosition();
            Quaternion rotation = Quaternion.identity; // look at player?

            Core.PoolController.Spawn((PoolType) type, position, rotation, onDeactivate: OnEnemyKilled);
            Core.PoolController.Spawn(PoolType.EnemySpawnEffect, position, rotation);
			Core.SfxController.Play(SfxSystem.SfxType.EnemySpawn, position);
        }

        private void OnEnemyKilled() {
			CurrentEnemyCount--;
		}

		public void Dispose() {}
	}
}