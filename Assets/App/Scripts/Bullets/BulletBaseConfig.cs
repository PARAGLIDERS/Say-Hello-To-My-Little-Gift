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
		Pistol = ObjectType.BulletExplosion_Pistol,
		Uzi = ObjectType.BulletExplosion_Uzi,
		Shotgun = ObjectType.BulletExplosion_Shotgun,
		Auto = ObjectType.BulletExplosion_Auto,
		DoubleShotgun = ObjectType.BulletExplosion_DoubleShotgun,
		RocketLauncher = ObjectType.BulletExplosion_RocketLauncher,
		Minigun = ObjectType.BulletExplosion_Minigun,

		Snowball = ObjectType.ProjectileExplosion_Snowball,
	}
}
