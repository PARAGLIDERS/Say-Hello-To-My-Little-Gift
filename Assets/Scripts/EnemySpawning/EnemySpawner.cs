using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using Misc.Root;
using PoolSystem;
using UnityEngine;

namespace EnemySpawning {
	public class EnemySpawner {
		private readonly EnemySpawnerConfig _config;
		private readonly EnemySpawnerPool _pool;
		private readonly EnemySpawnerGrid _grid;

		public Action OnChange;

		private Action _onAllEnemiesKilled;

		public int CurrentWaveEnemiesCountMax { get; private set; }
		
		private int _currentWaveEnemyCount;
		public int CurrentWaveEnemyCount {
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
		
		public EnemySpawner(Action onAllEnemiesKilled) {
			_config = Core.Resources.EnemySpawnerConfig;
			_grid = new EnemySpawnerGrid(Core.Resources.EnemySpawnerGridConfig);
			_pool = new EnemySpawnerPool(Core.Resources.EnemiesConfig);
			_onAllEnemiesKilled = onAllEnemiesKilled;
		}

		public void StartSpawning() {
			Core.CoroutineRunner.Run(Spawning());
		}

		public void StopSpawning() {
			Core.CoroutineRunner.Stop(Spawning());
		}
		
		private IEnumerator Spawning() {
			CurrentRound = 0;
			CurrentWave = 0;

			while (CurrentRound < _config.Rounds.Count) {
				EnemySpawnRound round = _config.Rounds[CurrentRound];
				yield return new WaitForSeconds(round.Delay);
				
				while (CurrentWave < round.Waves.Count) {
					EnemySpawnWave wave = round.Waves[CurrentWave];
					yield return new WaitForSeconds(wave.Delay);

					for (int i = 0; i < wave.EnemyCount; i++) {
						SpawnEnemy(wave.GetEnemyType());
						CurrentWaveEnemyCount++;
						yield return new WaitForSeconds(wave.Period);
					}

					float cooldown = _config.WaveCooldown;
					
					while (CurrentWaveEnemyCount > 0 || cooldown > 0) {
						cooldown -= Time.deltaTime;
						yield return null;
					}

					CurrentWave++;
				}
				
				CurrentRound++;
			}

			yield return new WaitUntil(() => CurrentWaveEnemyCount <= 0);
			_onAllEnemiesKilled?.Invoke();
		}

		private void SpawnEnemy(EnemyType type) {
			Enemy enemy = _pool.Get(type);

			if (enemy == null) {
				Debug.LogError($"enemy of type {type} is null");
				return;
			}
			
			enemy.OnKill += OnEnemyKilled;
			enemy.Activate(_grid.GetPosition());
		}

		private void OnEnemyKilled() {
			CurrentWaveEnemyCount--;
		}

		public void Dispose() {
			_pool.Dispose();
		}
	}
}