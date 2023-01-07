﻿using UnityEngine;

namespace SfxSystem {
	[RequireComponent(typeof(AudioSource))]
	public class Sfx : MonoBehaviour {
		private AudioSource _source;

		private void Awake() {
			_source = GetComponent<AudioSource>();
		}

		public void Play(AudioClip clip) {
			_source.Stop();
			_source.clip = clip;
			_source.pitch = Random.Range(0.8f, 1.2f);
			_source.Play();
		}
	}
}