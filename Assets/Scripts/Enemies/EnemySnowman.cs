using Root;
using Player;
using UnityEngine;

namespace Enemies {
    public class EnemySnowman : Enemy {
        [SerializeField] private float _fireDistance;
        [SerializeField] [Range(0.1f, 50f)] private float _fireRate;
        [SerializeField] private Transform _muzzle;

        private float _cooldown;

        public override void Activate(Vector3 position, Quaternion rotation) {
            base.Activate(position, rotation);
            _cooldown = 0;
        }

        protected override void Update() {
            base.Update();

            bool atFireDistance = Vector3.Distance(PlayerController.POSITION, transform.position) <= _fireDistance;
            bool cooldownFinish = _cooldown >= 1f / _fireRate;

            if (atFireDistance && cooldownFinish) {
                Core.PoolController.Spawn(PoolSystem.PoolType.Snowball, _muzzle.position, _muzzle.rotation);
                _cooldown = 0;
            }

            _cooldown += Time.deltaTime;
        }
    }
}
