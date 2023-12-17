using PoolSystem;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

namespace SfxSystem {
	[CreateAssetMenu(menuName = "Santa/Sfx/Sfx Controller Config" , fileName = "Sfx Config")]
	public class SfxControllerConfig : ScriptableObject {
        [SerializeField] private AudioMixerGroup _mixerGroup;
		[SerializeField] private List<SfxConfig> _configs;

        public AudioMixerGroup MixerGroup => _mixerGroup;

		public List<SfxConfigItem> GetItems() {
			List<SfxConfigItem> items = new List<SfxConfigItem>();

			foreach (var config in _configs) {
				items.AddRange(config.Items);
			}

			return items;
		}

		private void OnValidate() {
			HashSet<string> names = new HashSet<string>();
			foreach (var config in _configs) {
				if (!names.Add(config.name)) {
					Debug.LogError($"{config.name} is already added!");
				}
			}
		}
	}
}