using EnemySpawning;
using GunSystem;
using PoolSystem;
using SfxSystem;
using Ui;
using UnityEngine;

namespace Root {
	[CreateAssetMenu(menuName = "Santa/Resources")]
	public class Resources : ScriptableObject {
		[SerializeField] private EnemySpawnerConfig _enemySpawnerConfig;
		[SerializeField] private EnemySpawnerGridConfig _enemySpawnerGridConfig; 

		[SerializeField] private PoolConfig _poolConfig;
        [SerializeField] private SfxConfig _sfxConfig;

		[SerializeField] private Canvas _canvasPrefab;
		[SerializeField] private UiScreenConfig _screenConfig;

        [SerializeField] private GunsConfig _gunsConfig;
		
        public Canvas CanvasPrefab => _canvasPrefab;
		public UiScreenConfig ScreenConfig => _screenConfig;

		public PoolConfig PoolConfig => _poolConfig;
        public SfxConfig SfxConfig => _sfxConfig;

		public EnemySpawnerConfig EnemySpawnerConfig => _enemySpawnerConfig;
		public EnemySpawnerGridConfig EnemySpawnerGridConfig => _enemySpawnerGridConfig;

        public GunsConfig GunsConfig => _gunsConfig;
	}
}