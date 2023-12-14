using Root;
using UnityEngine;
using Units;

namespace Enemies {
    public class EnemySnowman : Enemy {
        [SerializeField] private float _fireDistance;
        [SerializeField] [Range(0.1f, 50f)] private float _fireRate;
        [SerializeField] private Transform _muzzle;
        [SerializeField] private float _rotationSpeed;

        private float _cooldown;
        private UnitRotation _rotation;

        private void Awake() {
            _agent.updateRotation = false;
            _rotation = new UnitRotation(transform, _rotationSpeed);
        }

        public override void Activate(Vector3 position, Quaternion rotation) {
            base.Activate(position, rotation);
            ResetCooldown();
        }

        protected override void Update() {
            base.Update();
            
            Rotate();
            UpdateCooldown();

            if (!IsNearPlayer()) return;
            if (!IsLookingAtPlayer()) return;
            if (!CanShoot()) return;

            Shoot();
            ResetCooldown();
        }

        private void Rotate() {
            Vector3 target = GetPlayerPosition();
            _rotation.Update(target);
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

        private bool IsNearPlayer() {
            Vector3 playerPosition = GetPlayerPosition();
            return Vector3.Distance(playerPosition, transform.position) <= _fireDistance;
        }
        
        private bool IsLookingAtPlayer() {
            Vector3 playerPosition = GetPlayerPosition();
            return _rotation.IsLookingAt(playerPosition);
        }

        private Vector3 GetPlayerPosition() {
            return Core.LevelController.PlayerController.Position;
        }
    }
}
