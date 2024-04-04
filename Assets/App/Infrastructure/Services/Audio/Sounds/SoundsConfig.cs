using System;
using UnityEngine;
using UnityEngine.Audio;
using Utils;

namespace SfxSystem {
	[CreateAssetMenu(menuName = Constants.MenuFolder + nameof(SoundsConfig))]
	public class SoundsConfig : ItemsConfig<SoundsConfigItem> {
		public AudioMixerGroup MixerGroup;
	}

	[Serializable]
	public class SoundsConfigItem : ItemsConfigItem {
		public override string Name => Type.ToString();

		public SfxType Type;
		public AudioClip Clip;

		[Range(0f, 1f)] public float Volume;
		[Range(0f, 1f)] public float PitchShift;
	}
}