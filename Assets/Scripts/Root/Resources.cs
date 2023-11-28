using CameraControl;
using DayNightCycle;
using EnemySpawning;
using GunSystem;
using Player;
using PoolSystem;
using SfxSystem;
using Ui;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Root {
	[CreateAssetMenu(menuName = "Santa/Resources")]
	public class Resources : ScriptableObject {
		[SerializeField] private EnemySpawnerConfig _enemySpawnerConfig;

		[SerializeField] private PoolConfig _poolConfig;
        [SerializeField] private SfxConfig _sfxConfig;

		[SerializeField] private Canvas _canvasPrefab;
		[SerializeField] private UiScreenConfig _screenConfig;

        [SerializeField] private GunsConfig _gunsConfig;
        [SerializeField] private PlayerController _playerPrefab;
        [SerializeField] private CameraController _cameraPrefab;

        [SerializeField] private PostProcessVolume _volumePrefab;
        [SerializeField] private DayNightConfig _dayNightConfig;

        public Canvas CanvasPrefab => _canvasPrefab;
		public UiScreenConfig ScreenConfig => _screenConfig;

		public PoolConfig PoolConfig => _poolConfig;
        public SfxConfig SfxConfig => _sfxConfig;

		public EnemySpawnerConfig EnemySpawnerConfig => _enemySpawnerConfig;

        public GunsConfig GunsConfig => _gunsConfig;
        public PlayerController PlayerPrefab => _playerPrefab;
        public CameraController CameraPrefab => _cameraPrefab;

        public PostProcessVolume VolumePrefab => _volumePrefab;
        public DayNightConfig DayNightConfig => _dayNightConfig;
    }
}