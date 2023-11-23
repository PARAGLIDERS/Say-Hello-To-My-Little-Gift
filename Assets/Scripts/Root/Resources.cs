using System.Collections.Generic;
using Enemies;
using EnemySpawning;
using PoolSystem;
using Ui;
using UnityEngine;

namespace Misc.Root {
	[CreateAssetMenu(menuName = "Santa/Resources")]
	public class Resources : ScriptableObject {
		[SerializeField] private EnemySpawnerConfig _enemySpawnerConfig;
		[SerializeField] private EnemiesConfig _enemiesConfig;
		[SerializeField] private EnemySpawnerGridConfig _enemySpawnerGridConfig; 

		[SerializeField] private PoolConfig _poolConfig;
		[SerializeField] private Canvas _canvasPrefab;
		[SerializeField] private List<UiScreenConfig> _screenConfigs;

		public Canvas CanvasPrefab => _canvasPrefab;
		public List<UiScreenConfig> ScreenConfigs => _screenConfigs;
		public List<Pool> Pools => _poolConfig.Pools;

		public EnemySpawnerConfig EnemySpawnerConfig => _enemySpawnerConfig;
		public EnemiesConfig EnemiesConfig => _enemiesConfig;
		public EnemySpawnerGridConfig EnemySpawnerGridConfig => _enemySpawnerGridConfig;
	}
}