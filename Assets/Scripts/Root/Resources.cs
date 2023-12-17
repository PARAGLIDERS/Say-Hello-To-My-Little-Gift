using CameraControl;
using DayNightCycle;
using GunSystem;
using Level;
using Music;
using Player;
using Pointer;
using PoolSystem;
using SfxSystem;
using Ui;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Root {
	[CreateAssetMenu(menuName = "Santa/Resources", fileName = "Resources")]
	public class Resources : ScriptableObject {
		[SerializeField] private PoolControllerConfig _poolControllerConfig;
        [SerializeField] private SfxControllerConfig _sfxConfig;
        [SerializeField] private MusicConfig _musicConfig;

		[SerializeField] private Canvas _canvasPrefab;
		[SerializeField] private UiScreenConfig _screenConfig;

        [SerializeField] private GunsControllerConfig _gunsConfig;
        [SerializeField] private GunsSpawnerConfig _gunSpawnerConfig;
        [SerializeField] private PlayerController _playerPrefab;
        [SerializeField] private CameraController _cameraPrefab;

        [SerializeField] private PostProcessVolume _volumePrefab;
        [SerializeField] private DayNightConfig _dayNightConfig;

        [SerializeField] private LevelsConfig _levelsConfig;

        [SerializeField] private PointerControllerConfig _pointerControllerConfig;

        public Canvas CanvasPrefab => _canvasPrefab;
		public UiScreenConfig ScreenConfig => _screenConfig;

		public PoolControllerConfig PoolControllerConfig => _poolControllerConfig;
        public SfxControllerConfig SfxConfig => _sfxConfig;

        public GunsControllerConfig GunsConfig => _gunsConfig;
        public GunsSpawnerConfig GunsSpawnerConfig => _gunSpawnerConfig;

        public PlayerController PlayerPrefab => _playerPrefab;
        public CameraController CameraPrefab => _cameraPrefab;

        public PostProcessVolume VolumePrefab => _volumePrefab;
        public DayNightConfig DayNightConfig => _dayNightConfig;

        public MusicConfig MusicConfig => _musicConfig;

        public LevelsConfig LevelsConfig => _levelsConfig;

        public PointerControllerConfig PointerControllerConfig => _pointerControllerConfig;
	}
}