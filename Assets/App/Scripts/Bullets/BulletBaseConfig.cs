using Pooling;
using UnityEngine;

namespace Bullets {
	public abstract class BulletBaseConfig : ScriptableObject {
		[SerializeField] private int _damage;
		[SerializeField] private float _lifetime;
		[SerializeField] private BulletExplosionType _explosionType;

		public int Damage => _damage;
		public float Lifetime => _lifetime;
		public BulletExplosionType ExplosionType => _explosionType;
	}

	public enum BulletExplosionType {
		Pistol = PoolType.BulletExplosion_Pistol,
		Uzi = PoolType.BulletExplosion_Uzi,
		Shotgun = PoolType.BulletExplosion_Shotgun,
		Auto = PoolType.BulletExplosion_Auto,
		DoubleShotgun = PoolType.BulletExplosion_DoubleShotgun,
		RocketLauncher = PoolType.BulletExplosion_RocketLauncher,
		Minigun = PoolType.BulletExplosion_Minigun,

		Snowball = PoolType.ProjectileExplosion_Snowball,
	}
}
