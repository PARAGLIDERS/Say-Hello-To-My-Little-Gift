using PoolSystem;
using Root;
using SfxSystem;
using UnityEngine;

namespace Pickupables {
	public abstract class Pickupbable : PoolObject {
		[SerializeField] private SfxType _pickupSfx;
		[SerializeField] private PoolType _pickupEffect;

		private const float _distanceToPlayer = 50f;
		private const float _lifetime = 25f;
		
		private float _currentLifetime;

		public override void Activate(Vector3 position, Quaternion rotation) {
			base.Activate(position, rotation);
			ResetTimer();
		}

		protected abstract void HandlePickup(Collider other);

		// configured collision mask so it only triggers by the player 
		private void OnTriggerEnter(Collider other) {
			HandlePickup(other);
			Core.SfxController.Play(_pickupSfx);
			Despawn();
		}

		private void Update() {
			if (Time.time >= _currentLifetime) {
				if (IsNearPlayer()) {
					ResetTimer();
					return;
				}

				Despawn();
			}
		}

		private bool IsNearPlayer() {
			Vector3 playerPosition = Core.LevelController.Player.Position;
			return Vector3.Distance(transform.position, playerPosition) <= _distanceToPlayer;
		}

		private void ResetTimer() {
			_currentLifetime = Time.time + _lifetime;
		}

		private void Despawn() {
			Core.PoolController.Spawn(_pickupEffect, transform.position, Quaternion.identity);
			Deactivate();
		}
	}
}
