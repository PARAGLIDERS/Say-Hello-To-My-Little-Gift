using UnityEngine;

namespace DamageSystem {
	public class ExplosionRadiusDrawer : MonoBehaviour {
		[SerializeField] private ExplosionConfig _config;

		private void OnDrawGizmos() {
			Gizmos.DrawWireSphere(transform.position, _config.Radius);	
		}
	}
}
