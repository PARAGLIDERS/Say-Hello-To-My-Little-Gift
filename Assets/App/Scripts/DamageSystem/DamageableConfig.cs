using PoolSystem;
using SfxSystem;
using UnityEngine;

namespace DamageSystem {
	[CreateAssetMenu(menuName = "Santa/Damageable/Damageable Config")]
	public class DamageableConfig : ScriptableObject {
		[SerializeField] private int _maxHealth = 100;
		[SerializeField] private PoolType _dieParticles = PoolType.VFX_BloodParticles;
		[SerializeField] private PoolType _dieFloorDecals = PoolType.VFX_FloorBlood;
		[SerializeField] private SfxType _dieSfx = SfxType.BloodParticles;
		[SerializeField] private float _deathDelay;

		public int MaxHealth => _maxHealth;
		public PoolType DieParticles => _dieParticles;
		public PoolType DieFloorDecals => _dieFloorDecals;
		public SfxType DieSfx => _dieSfx;
		public float DeathDelay => _deathDelay;
	}
}
