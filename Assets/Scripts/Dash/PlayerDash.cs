using Root;
using UnityEngine;

namespace Dash {
	public class PlayerDash {
		private readonly PlayerDashConfig _config;
		private readonly Rigidbody _rigidbody;
		private readonly ParticleSystem _particles;
		private float _timer;

		public PlayerDash(PlayerDashConfig config, Rigidbody rigidbody, ParticleSystem particles) {
			_config = config;
			_rigidbody = rigidbody;
			_particles = particles;
		}

		public void Reset() {
			_timer = 0;
		}

		public void Update() {
			if(Time.time < _timer) return;

			if (Core.InputController.GetPlayerDashInput()) {
				Activate();
				_timer = Time.time + _config.Time;
			}	
		}	
		
		private void Activate() {
			Vector3 input = Core.InputController.GetPlayerInput();
			_rigidbody.AddForce(input * _config.Magnitude, ForceMode.Impulse);
			_particles.Play();
		}
	}
}
