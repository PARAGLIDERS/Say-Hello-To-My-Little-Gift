using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemySpawning {
	[CreateAssetMenu(menuName = "Enemy Spawner Config")]
	public class EnemySpawnerConfig : ScriptableObject {
		[SerializeField] private List<EnemySpawnRound> _rounds;

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

		public EnemyType GetEnemyType() {
			int chanceSum = _units.Sum(unit => unit.Chance);
			float rand = Random.Range(0f, chanceSum);

			EnemyType type = default;
			foreach (EnemySpawnWaveUnit unit in _units) {
				if(unit.Chance > rand) continue;
				type = unit.Type;
			}

			return type;
		}
	}

	[Serializable]
	public struct EnemySpawnWaveUnit {
		[SerializeField] private EnemyType _type;
		[SerializeField] private int _chance;

		public EnemyType Type => _type;
		public int Chance => _chance;
	}

	public enum EnemyType {
		Snowman,
		Chicken,
		Deer,
		Owl,
		Penguin,
		Rabbit,
	}
}