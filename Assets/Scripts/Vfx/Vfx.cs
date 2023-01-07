using PoolSystem;
using UnityEngine;

namespace Vfx {
	[RequireComponent(typeof(ParticleSystem))]
	public class Vfx : PoolObject {
		private ParticleSystem _particleSystem;
		private float _timer;
		
		private void Awake() {
			_particleSystem = GetComponent<ParticleSystem>();
		}

		private void OnEnable() {
			_timer = _particleSystem.main.duration;
			_particleSystem.Play();
		}

		private void Update() {
			if (_timer > 0) {
				_timer -= Time.deltaTime;
				return;
			}
			
			Deactivate();
		}
	}
}