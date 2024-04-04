using Pooling;
using SfxSystem;
using UnityEngine;

namespace DamageSystem {
	[CreateAssetMenu(menuName = "Santa/Damageable/Damageable Config")]
	public class DamageableConfig : ScriptableObject {
		[SerializeField] private int _maxHealth = 100;
		[SerializeField] private ObjectType _dieParticles = ObjectType.VFX_BloodParticles;
		[SerializeField] private ObjectType _dieFloorDecals = ObjectType.VFX_FloorBlood;
		[SerializeField] private SfxType _dieSfx = SfxType.BloodParticles;
		[SerializeField] private float _deathDelay;

		public int MaxHealth => _maxHealth;
		public ObjectType DieParticles => _dieParticles;
		public ObjectType DieFloorDecals => _dieFloorDecals;
		public SfxType DieSfx => _dieSfx;
		public float DeathDelay => _deathDelay;
	}
}
