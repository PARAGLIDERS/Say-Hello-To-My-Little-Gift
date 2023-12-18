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
		public event Action RoundStarted;
		public event Action RoundFinished;
		public event Action EnemyKilled;
		public event Action AllEnemiesKilled;


		public int CurrentEnemyCount {get; private set;}
		public int MaxEnemies {get; private set;}

		public int CurrentRound {get; private set; }
		public int MaxRounds => _config.Rounds.Count;

		private EnemySpawnerConfig _config;
		private SpawnerGrid _grid;
        private IEnumerator _execution;

		public EnemySpawner() {}

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
            CurrentEnemyCount = 0;
        }

        public void Start(EnemySpawnerConfig config) {
			_config = config;
			_grid = new SpawnerGrid(config.GridConfig);

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

				CurrentEnemyCount = round.GetEnemiesCount();
				MaxEnemies = CurrentEnemyCount;

				RoundStarted?.Invoke();
				int currentWave = 0;

                while (currentWave < round.Waves.Count) {
                    wave = round.Waves[currentWave];
                    Randomizer<EnemySpawnWaveUnit> randomizer = new Randomizer<EnemySpawnWaveUnit>(wave.Units);

                    yield return new WaitForSeconds(wave.Delay);

					for (int i = 0; i < wave.EnemyCount; i++) {
                        //Spawn(randomizer.GetItem().Type);
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

                    currentWave++;
				}
				
				yield return new WaitUntil(() => CurrentEnemyCount <= 0);
				CurrentRound++;
				RoundFinished?.Invoke();
			}

			yield return new WaitForSeconds(1.5f);
			
            AllEnemiesKilled?.Invoke();
		}
                
        private void Spawn(EnemySpawnerUnitType type) {
            Vector3 position = _grid.GetPosition();
            Quaternion rotation = Quaternion.identity; // look at player? or away?)

            Core.PoolController.Spawn((PoolType) type, position, rotation, onDeactivate: HandleEnemyKill);
            Core.PoolController.Spawn(PoolType.VFX_EnemySpawnEffect, position, rotation);

			float distance = Vector3.Distance(position, Core.LevelController.Player.Position);
			if (distance > 50f) return;
			Core.SfxController.Play(SfxSystem.SfxType.EnemySpawn, position);
        }

        private void HandleEnemyKill() {
			CurrentEnemyCount--;
			EnemyKilled?.Invoke();
		}

		public void Dispose() {}
	}
}