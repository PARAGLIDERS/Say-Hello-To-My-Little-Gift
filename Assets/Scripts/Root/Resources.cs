using System.Collections.Generic;
using Enemies;
using EnemySpawning;
using PoolSystem;
using SfxSystem;
using Ui;
using UnityEngine;

namespace Misc.Root {
	[CreateAssetMenu(menuName = "Santa/Resources")]
	public class Resources : ScriptableObject {
		[SerializeField] private EnemySpawnerConfig _enemySpawnerConfig;
		[SerializeField] private EnemySpawnerGridConfig _enemySpawnerGridConfig; 

		[SerializeField] private PoolConfig _poolConfig;
        [SerializeField] private SfxConfig _sfxConfig;

		[SerializeField] private Canvas _canvasPrefab;
		[SerializeField] private UiScreenConfig _screenConfig;

		public Canvas CanvasPrefab => _canvasPrefab;
		public UiScreenConfig ScreenConfig => _screenConfig;

		public PoolConfig PoolConfig => _poolConfig;
        public SfxConfig SfxConfig => _sfxConfig;

		public EnemySpawnerConfig EnemySpawnerConfig => _enemySpawnerConfig;
		public EnemySpawnerGridConfig EnemySpawnerGridConfig => _enemySpawnerGridConfig;
	}
}