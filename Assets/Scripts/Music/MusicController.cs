using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

namespace Music {
	public class MusicController {
		private readonly AudioSource _source;
		private readonly Dictionary<MusicClipType, MusicConfigItem> _clips;

		public MusicController(Transform parent, MusicConfig config) {
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
		}

		public void Play(MusicClipType type, bool loop = true) {
			if(!_clips.TryGetValue(type, out MusicConfigItem item)) {
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
			//_source.DOKill();
			//_source.DOFade(0f, 0.3f).OnComplete();
		}
	}
}
