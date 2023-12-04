using PoolSystem;
using Root;
using UnityEngine;

namespace GunSystem {
    public class GunPickup : PoolObject {
        [SerializeField] private Mesh _mesh;
        [SerializeField] private GunType _type;
        [SerializeField] private int _ammo;

        private const float _lifetime = 25f;
        private float _currentLifetime;

        public override void Activate(Vector3 position, Quaternion rotation) {
            base.Activate(position, rotation);
            Core.PoolController.Spawn(PoolType.GunPickupActivateEffect, transform.position, Quaternion.identity);
            _currentLifetime = Time.time + _lifetime;
        }

        // configured collision mask so it only triggers by the player 
        private void OnTriggerEnter(Collider other) {
            Core.LevelController.GunsController.Pickup(_type, _ammo);
            Core.SfxController.Play(SfxSystem.SfxType.VfxGunPickup);
            Deactivate();
        }

        private void Update() {
            if(Time.time >= _currentLifetime) {
                Deactivate();
                Core.PoolController.Spawn(PoolType.GunPickupDeactivateEffect, transform.position, Quaternion.identity);
            }
        }
    }
}
