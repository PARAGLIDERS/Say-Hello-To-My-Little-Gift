using UnityEngine;

namespace DamageSystem {
	public class ExplosionRadiusDrawer : MonoBehaviour {
		[SerializeField] private ExplosionConfig _config;

		private void OnDrawGizmos() {
			if( _config != null ) {
				Gizmos.DrawWireSphere(transform.position, _config.Radius);	
			}
		}
	}
}
