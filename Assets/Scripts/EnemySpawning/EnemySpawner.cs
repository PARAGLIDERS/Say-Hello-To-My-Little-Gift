using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using Misc.Root;
using PoolSystem;
using Units;
using UnityEngine;

namespace EnemySpawning {
	public class EnemySpawner : MonoBehaviour {
		[SerializeField] private EnemySpawnerGrid _grid;
		[SerializeField] private EnemySpawnerConfig _config;
		[SerializeField] private EnemiesConfig _enemiesConfigs;

		private Dictionary<EnemyType, EnemyConfig> _enemyDictionary;

		public Action<int> OnRoundChange;
		public Action<int> OnWaveChange;
		public Action<int> OnEnemyCountChange;
		
		private Action _onAllEnemiesKilled;
		private Action _onFinishSpawning;

		private int _currentEnemyCount;

		private int CurrentEnemyCount {
			get => _currentEnemyCount;
			set {
				_currentEnemyCount = value;
				OnEnemyCountChange?.Invoke(_currentEnemyCount);
			}
		}
		
		public void Init(Action onAllEnemiesKilled, Action onFinishSpawning) {
			_grid.Init();
			_onAllEnemiesKilled = onAllEnemiesKilled;
			_onFinishSpawning = onFinishSpawning;
			
			FillEnemyDictionary();
		}

		private void FillEnemyDictionary() {
			foreach (EnemyConfig config in _enemiesConfigs.EnemyConfigs) {
				_enemyDictionary.TryAdd(config.Type, config);
			}
		}

		public void StartSpawning() {
			StartCoroutine(Spawning());
		}

		public void StopSpawning() {
			StopCoroutine(Spawning());
		}
		
		private IEnumerator Spawning() {
			int currentRound = 0;
			int currentWave = 0;
			
			while (currentRound < _config.Rounds.Count) {
				EnemySpawnRound round = _config.Rounds[currentRound];
				yield return new WaitForSeconds(round.Delay);
				
				while (currentWave < round.Waves.Count) {
					EnemySpawnWave wave = round.Waves[currentWave];
					yield return new WaitForSeconds(wave.Delay);
					
					CurrentEnemyCount = wave.EnemyCount;

					SpawnEnemy(wave.GetEnemyType());
				}
				
				currentRound++;
			}
			
			_onFinishSpawning?.Invoke();
		}

		private void SpawnEnemy(EnemyType type) {
			if (!_enemyDictionary.TryGetValue(type, out EnemyConfig config)) {
				Debug.LogError($"enemy spawner can't get {type} from dictionary");
				return;
			}
			
			Enemy enemy = Core.PoolController.Spawn<Enemy>(PoolType.Enemy);
			enemy.Init(config, OnEnemyKilled);
			enemy.Activate(_grid.GetPosition(), Quaternion.identity);
		}

		private void OnEnemyKilled() {
			CurrentEnemyCount--;
		}
	}
}