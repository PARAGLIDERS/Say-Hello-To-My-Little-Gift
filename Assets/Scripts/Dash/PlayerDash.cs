using Root;
using System;
using UnityEngine;

namespace Dash {
	public class PlayerDash {
		public event Action Changed;
		public int Count { get; private set; }

		private readonly PlayerDashConfig _config;
		private readonly Rigidbody _rigidbody;
		private readonly ParticleSystem _particles;

		private float _cooldown;
		private float _restore;

		public PlayerDash(PlayerDashConfig config, Rigidbody rigidbody, ParticleSystem particles) {
			_config = config;
			_rigidbody = rigidbody;
			_particles = particles;
		}

		public void Reset() {
			Count = _config.Count;
			_restore = _config.Restore;
			_cooldown = 0;
		}

		public void Update() {
			if (Time.time >= _restore && Count < _config.Count) {
				Count++;
				_restore = Time.time + _config.Restore;
				Changed?.Invoke();
			}

			if (Time.time < _cooldown) return;
			if (Core.InputController.GetPlayerDashInput()) {
				if (Count > 0) {
					Activate();
					_restore = Time.time + _config.Restore;
					_cooldown = Time.time + _config.Time;
				}
			}	
		}	
		
		private void Activate() {
			Vector3 input = Core.InputController.GetPlayerInput();
			_rigidbody.AddForce(input * _config.Magnitude, ForceMode.Impulse);
			_particles.Play();
			Count--;
			Changed?.Invoke();
			Core.SfxController.Play(SfxSystem.SfxType.Dash);
		}
	}
}
