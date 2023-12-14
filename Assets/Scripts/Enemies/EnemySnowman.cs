using Root;
using UnityEngine;
using Units;

namespace Enemies {
    public class EnemySnowman : Enemy {
        [SerializeField] [Range(0.1f, 50f)] private float _fireRate;
        [SerializeField] private Transform _muzzle;

        private float _cooldown;

        public override void Activate(Vector3 position, Quaternion rotation) {
            base.Activate(position, rotation);
            ResetCooldown();
        }

        protected override void Update() {
            base.Update();
            
            UpdateCooldown();

            if (!IsNearPlayer()) return;
            if (!IsLookingAtPlayer()) return;
            if (!CanShoot()) return;

            Shoot();
            ResetCooldown();
        }

        private void UpdateCooldown() {
            _cooldown += Time.deltaTime;
        }

        private void ResetCooldown() {
            _cooldown = 0;
        }

        private bool CanShoot() {
            return _cooldown >= 1f / _fireRate;
        }

        private void Shoot() {
            Core.PoolController.Spawn(PoolSystem.PoolType.EnemyProjectile_Snowball, _muzzle.position, _muzzle.rotation);
            Core.SfxController.Play(SfxSystem.SfxType.EnemySnowmanThrow, _muzzle.position);
        }
    }
}
