using CameraControl;
using GunSystem;
using Heals;
using Level;
using Music;
using Player;
using Pointer;
using Pooling;
using SfxSystem;
using Ui;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Root {
	[CreateAssetMenu(menuName = "Santa/Resources", fileName = "Resources")]
	public class Resources : ScriptableObject {
		public PoolsConfig PoolControllerConfig;
        public SoundsConfig SfxConfig;
        public MusicConfig MusicConfig;

		public Canvas CanvasPrefab;
		public UiScreenConfig ScreenConfig;

        public GunsControllerConfig GunsConfig;
        public GunsSpawnerConfig GunSpawnerConfig;
        public PlayerController PlayerPrefab;
        public CameraController CameraPrefab;

        public PostProcessVolume VolumePrefab;

        public LevelsConfig LevelsConfig;

        public PointerControllerConfig PointerControllerConfig;

        public HealSpawnerConfig HealSpawnerConfig;       
	}
}