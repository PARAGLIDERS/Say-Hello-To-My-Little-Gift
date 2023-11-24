using System;
using System.Collections.Generic;
using UnityEngine;

namespace SfxSystem {
	[CreateAssetMenu(menuName = "sfx config")]
	public class SfxConfig : ScriptableObject {
		[SerializeField] private List<SfxConfigItem> _items;
        public List<SfxConfigItem> Items => _items;
	}

	[Serializable]
	public class SfxConfigItem {
        [SerializeField] private SfxType _type;
        [SerializeField] private AudioClip _clip;
        [SerializeField] [Range(0f, 1f)] private float _volume;
        [SerializeField] [Range(0f, 1f)] private float _pitchShift;

        public SfxType Type => _type;
		public AudioClip Clip => _clip;
        public float Volume => _volume;
        public float PitchShift => _pitchShift;
	}
	
	public enum SfxType {
        // ui
        UiButtonPress = 0,
        UiButtonSelect = 1,

        // shots
		ShotPistol = 100,
        ShotAuto = 101,
        ShotShotgun = 102,

        // vfx
        VfxExplosion = 300,
        VfxBloodParticles = 301,
	}
}