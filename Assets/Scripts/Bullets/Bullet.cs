using DamageSystem;
using Root;
using PoolSystem;
using UnityEngine;

namespace Bullets {
	[RequireComponent(typeof(Rigidbody))]
	public class Bullet : BulletBase {
		[SerializeField] private float _speedMin = 80f;
		[SerializeField] private float _speedMax = 120f;
		[SerializeField] private TrailRenderer _trail;

        protected override PoolType _explosionType => PoolType.BulletExplosion;

        public override void Activate(Vector3 position, Quaternion rotation) {
            _trail.Clear();
            base.Activate(position, rotation);
            _rigidbody.AddForce(Random.Range(_speedMin, _speedMax) * transform.forward, ForceMode.Impulse);
        }
    }
}