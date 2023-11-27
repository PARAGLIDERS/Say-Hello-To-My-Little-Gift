using System;
using System.Collections.Generic;
using PoolSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemySpawning {
	[CreateAssetMenu(menuName = "Enemy Spawner Config")]
	public class EnemySpawnerConfig : ScriptableObject {
		[SerializeField] private float _waveCooldown; // time between wave N finish and wave N+1 start
		[SerializeField] private List<EnemySpawnRound> _rounds;

		public float WaveCooldown => _waveCooldown;
		public List<EnemySpawnRound> Rounds => _rounds;
	}

	[Serializable]
	public struct EnemySpawnRound {
		[SerializeField] private float _delay;
		[SerializeField] private List<EnemySpawnWave> _waves;

		public float Delay => _delay;
		public List<EnemySpawnWave> Waves => _waves;
	}
	
	[Serializable]
	public struct EnemySpawnWave {
		[SerializeField] private float _delay;
		[SerializeField] private int _enemyCount;
		[SerializeField] private float _period;
		[SerializeField] private List<EnemySpawnWaveUnit> _units;

		public float Delay => _delay;
		public int EnemyCount => _enemyCount;
		public float Period => _period;

		public PoolType GetEnemyType() {
			int random = Random.Range(1, 101);

			List<PoolType> possibleTypes = new();
			for (int i = 0; i < _units.Count; i++) {
				if (_units[i].Chance < random) continue;
				possibleTypes.Add(_units[i].Type);
			}
			
			PoolType type = default;

			if (possibleTypes.Count > 0) {
				type = possibleTypes[Random.Range(0, possibleTypes.Count)];
			}
			
			return type;
		}
	}

	[Serializable]
	public struct EnemySpawnWaveUnit {
		[SerializeField] private PoolType _type;
		[SerializeField] [Range(1, 100)] private int _chance;

		public PoolType Type => _type;
		public int Chance => _chance;
	}
}