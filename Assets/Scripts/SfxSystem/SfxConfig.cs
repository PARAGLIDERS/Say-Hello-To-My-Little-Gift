using System;
using System.Collections.Generic;
using UnityEngine;

namespace SfxSystem {
	[CreateAssetMenu(menuName = "Santa/Sfx/Sfx Config", fileName = "Sfx Config")]
	public class SfxConfig : ScriptableObject {
		[SerializeField] private List<SfxConfigItem> _items;

		public List<SfxConfigItem> Items => _items;

		public void OnValidate() {
			foreach(var item in _items) {
				item.Validate();
			}
		}
	}

	[Serializable]
	public class SfxConfigItem {
		[HideInInspector] public string Name;

		[SerializeField] private SfxType _type;
		[SerializeField] private AudioClip _clip;
		[SerializeField][Range(0f, 1f)] private float _volume;
		[SerializeField][Range(0f, 1f)] private float _pitchShift;

		public SfxType Type => _type;
		public AudioClip Clip => _clip;
		public float Volume => _volume;
		public float PitchShift => _pitchShift;

		public void Validate() {
			Name = Type.ToString();
		}
	}
}
