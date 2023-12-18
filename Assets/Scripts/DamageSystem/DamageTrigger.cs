using UnityEngine;

namespace DamageSystem {
	public class DamageTrigger : MonoBehaviour {
		[SerializeField] private int _damage;
		[SerializeField] private Damageable _self;

		private void OnTriggerEnter(Collider other) {
			if (other.TryGetComponent(out Damageable damageable)) {
				if(damageable != _self) {
					damageable.ApplyDamage(_damage, transform.rotation);
				}
			}	
		}
	}
}
