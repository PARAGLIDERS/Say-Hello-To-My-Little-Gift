using Data;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Music {
	public class MusicPlayer {
		private readonly DataService _dataService;
		private readonly MusicConfig _config;
		private readonly AudioSource _source;
		private readonly Dictionary<MusicClipType, MusicConfigItem> _clips;


		public MusicPlayer(DataService dataService, Transform parent, MusicConfig config) {
			_dataService = dataService;
			_config = config;

			GameObject sourceObject = new GameObject("MusicPlayer");
			sourceObject.transform.SetParent(parent);

			_source = sourceObject.AddComponent<AudioSource>();
			_source.outputAudioMixerGroup = config.MixerGroup;
			_source.playOnAwake = false;
			_source.volume = 0;

			_clips = new Dictionary<MusicClipType, MusicConfigItem>();
			foreach (MusicConfigItem item in config.Clips) {
				if (_clips.ContainsKey(item.Type)) {
					Debug.LogError($"clip already exists in music dictionary: {item.Type}");
					continue;
				}

				_clips.Add(item.Type, item);
			}

			_dataService.OnSave += ApplySettings;
		}

		public void Play(MusicClipType type, bool loop = true) {
			if (!_clips.TryGetValue(type, out MusicConfigItem item)) {
				Debug.LogError($"music is not found: {type}");
				return;
			}

			_source.Stop();
			_source.clip = item.Clip;
			_source.Play();
			_source.loop = loop;

			_source.DOKill();
			_source.DOFade(item.Volume, 0.3f);
		}

		public void Stop() {
			_source.Stop();
		}

		public void Dispose() {
			_dataService.OnSave -= ApplySettings;
		}

		private void ApplySettings() {
			SliderSetting setting = _dataService.Data.Settings.Audio.MusicVolume;
			float volume = setting.Current;
			if (volume == setting.Min)
				volume = -80f;
			_config.MixerGroup.audioMixer.SetFloat("MusicVolume", volume);
		}
	}
}
