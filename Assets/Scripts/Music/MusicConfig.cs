using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Music {
	[CreateAssetMenu(menuName = "Santa/MusicConfig", fileName = "MusicConfig")]
	public class MusicConfig : ScriptableObject {
		[SerializeField] private AudioMixerGroup _mixerGroup;
		[SerializeField] private List<MusicConfigItem> _clips;

		public List<MusicConfigItem> Clips => _clips;
		public AudioMixerGroup MixerGroup => _mixerGroup;

		private void OnValidate() {
			foreach (MusicConfigItem item in _clips) {
				item.Validate();
			}
		}
	}

	[Serializable]
	public class MusicConfigItem {
		[HideInInspector] public string name;

		[SerializeField] private MusicClipType _type;
		[SerializeField] private AudioClip _clip;
		[SerializeField] [Range(0f, 1f)] private float _volume;

		public MusicClipType Type => _type;
		public AudioClip Clip => _clip;
		public float Volume => _volume;

		public void Validate() {
			name = _type.ToString();
		}
	}

	public enum MusicClipType {
		Menu = 0,
		Level1 = 1,
		Level2 = 2,
		Level3 = 3,
		PlayerDeath = 4,
	}
}
