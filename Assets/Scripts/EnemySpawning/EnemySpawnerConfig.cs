using System;
using System.Collections.Generic;
using UnityEngine;

namespace EnemySpawning {
	[CreateAssetMenu(menuName = "Enemy Spawner Config")]
	public class EnemySpawnerConfig : ScriptableObject {
		[SerializeField] private List<EnemySpawnRound> _rounds;

		public List<EnemySpawnRound> Rounds => _rounds;
	}

	[Serializable]
	public struct EnemySpawnRound {
		[SerializeField] private List<EnemySpawnWave> _waves;

		public List<EnemySpawnWave> Waves => _waves;
	}
	
	[Serializable]
	public struct EnemySpawnWave {
		[SerializeField] private int _enemyCount;
		[SerializeField] private float _period;
		[SerializeField] private List<EnemySpawnWaveUnit> _units; 
		
		public int EnemyCount => _enemyCount;
		public float Period => _period;
		public List<EnemySpawnWaveUnit> Units => _units;
	}

	[Serializable]
	public struct EnemySpawnWaveUnit {
		[SerializeField] private EnemyType _type;
		[SerializeField] [Range(0f, 1f)] private float _chance;

		public EnemyType Type => _type;
		public float Chance => _chance;
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