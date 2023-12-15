using DamageSystem;
using UnityEngine;
using Utils;

namespace Enemies {
    public class EnemyDeer : Enemy {
		[SerializeField] private Explosion _explosion;
		[SerializeField] private ParticleSystem _effect;
		
		protected override void Attack() {
			_effect.Play();
			_explosion.Activate();
		}
	}
}
