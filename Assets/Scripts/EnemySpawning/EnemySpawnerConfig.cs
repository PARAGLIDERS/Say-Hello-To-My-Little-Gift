using System;
using System.Collections.Generic;
using System.Linq;
using Grid;
using PoolSystem;
using RandomSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemySpawning {
	[CreateAssetMenu(menuName = "Santa/Enemy Spawner Config", fileName = "Enemy Spawner Config")]
	public class EnemySpawnerConfig : ScriptableObject {
		[SerializeField] [Tooltip("time between wave N finish and wave N+1 start")] 
        private float _waveCooldown; 

        [SerializeField] private SpawnerGridConfig _gridConfig;
		[SerializeField] private List<EnemySpawnRound> _rounds;

        public SpawnerGridConfig GridConfig => _gridConfig;
		public float WaveCooldown => _waveCooldown;
		public List<EnemySpawnRound> Rounds => _rounds;

        private void OnValidate() {
            if (_waveCooldown < 0) _waveCooldown = 0;

            for (int i = 0; i < _rounds.Count; i++) {
                _rounds[i].Validate(i);
            }
        }
    }

	[Serializable]
	public class EnemySpawnRound : EnemySpawnerConfigItem {
		[SerializeField] private float _delay;
		[SerializeField] private List<EnemySpawnWave> _waves;

		public float Delay => _delay;
		public List<EnemySpawnWave> Waves => _waves;

        public override void Validate(int index) {
            if (_delay < 0) _delay = 0; 

            _name = $"Round {index + 1}";

            for (int i = 0; i < _waves.Count; i++) {
                _waves[i].Validate(i);
            }
        }
	}
	
	[Serializable]
	public class EnemySpawnWave : EnemySpawnerConfigItem {
        [SerializeField] private float _delay;
		[SerializeField] private int _enemyCount;
		[SerializeField] private float _period;
		[SerializeField] private List<EnemySpawnWaveUnit> _units;

		public float Delay => _delay;
		public int EnemyCount => _enemyCount;
		public float Period => _period;
        public List<EnemySpawnWaveUnit> Units => _units;

        public override void Validate(int index) {
            if (_delay < 0) _delay = 0;
            if (_enemyCount < 0) _enemyCount = 0;
            if (_period < 0) _period = 0;

            _name = $"Wave {index + 1}";
            int sum = _units.Sum(x => x.Chance);

            for (int i = 0; i < _units.Count; i++) {
                _units[i].Validate((int) (100 * (float)_units[i].Chance / sum));
            }
        }
    }

	[Serializable]
	public class EnemySpawnWaveUnit : EnemySpawnerConfigItem, IRandomizerItem {
        [SerializeField] private EnemySpawnerUnitType _type;
		[SerializeField] [Range(1, 100)] private int _chance;

		public EnemySpawnerUnitType Type => _type;
		public int Chance => _chance;

        public override void Validate(int index) {
            _name = $"{_type} : {index}%";
        }
    }

    public abstract class EnemySpawnerConfigItem {
        [HideInInspector] public string _name;
        public abstract void Validate(int index);
    }

    public enum EnemySpawnerUnitType {
        Snowman = PoolType.Snowman,
        Chicken = PoolType.Chicken,
        Deer = PoolType.Deer,
        Owl = PoolType.Owl,
        Penguin = PoolType.Penguin,
        Rabbit = PoolType.Rabbit,
    }
}