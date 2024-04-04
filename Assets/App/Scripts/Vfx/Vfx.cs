using Pooling;
using UnityEngine;

namespace Vfx {
	[RequireComponent(typeof(ParticleSystem))]
	public class Vfx : PoolObject {
		[SerializeField] private ParticleSystem _particleSystem;
		private float _timer;

        public override void Activate(Vector3 position, Quaternion rotation) {
            base.Activate(position, rotation);
            _particleSystem.Play();
            _timer = Time.time + _particleSystem.main.duration;
        }

        private void Update() {
            if (Time.time < _timer) return;
			Deactivate();
		}
	}
}