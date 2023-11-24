using UnityEngine;

namespace SfxSystem {
	public class Sfx : MonoBehaviour {
		private AudioSource _source;

		public void Init(AudioSource source) {
            _source = source;
		}

		public void Play(AudioClip clip, float volume, float pitchShift, bool is3d) {
			_source.Stop();
			_source.clip = clip;
            _source.volume = volume;
			_source.pitch = 1f + Random.Range(-pitchShift, pitchShift);
            _source.spatialBlend = is3d ? 1 : 0;
			_source.Play();
		}
	}
}