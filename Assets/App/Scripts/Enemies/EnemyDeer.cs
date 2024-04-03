using DamageSystem;
using UnityEngine;
using Utils;

namespace Enemies {
    public class EnemyDeer : Enemy {
		[SerializeField] private Swing _swing;
		[SerializeField] private ParticleSystem _effect;
		
		protected override void Attack() {
			//_effect.Play();
			_swing.Activate();
		}
	}
}
