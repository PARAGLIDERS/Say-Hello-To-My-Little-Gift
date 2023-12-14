using PoolSystem;
using Root;
using UnityEngine;

namespace GunSystem {
    public class GunPickupable : PoolObject {
        [SerializeField] private GunType _type;
        [SerializeField] private float _lifetime = 25f;
        [SerializeField] private const float _distanceToPlayer = 50f;
        
        private float _currentLifetime;

		public override void Activate(Vector3 position, Quaternion rotation) {
			base.Activate(position, rotation);
            ResetTimer();
		}

		// configured collision mask so it only triggers by the player 
		private void OnTriggerEnter(Collider other) {
            Core.LevelController.GunsController.Pickup(_type);
            Core.SfxController.Play(SfxSystem.SfxType.VfxGunPickup);
            
            Despawn();
        }

        private void Update() {
            if(Time.time >= _currentLifetime) {
                if(IsNearPlayer()) {
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
			Core.PoolController.Spawn(PoolType.VFX_GunPickupVfx, transform.position, Quaternion.identity);
			Deactivate();
		}
	}
}
