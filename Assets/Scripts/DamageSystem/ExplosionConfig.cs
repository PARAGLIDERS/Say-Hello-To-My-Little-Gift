using UnityEngine;

namespace DamageSystem {
	[CreateAssetMenu(menuName ="Santa/Explosion/Explosion Config")]
	public class ExplosionConfig : ScriptableObject {
		[SerializeField] private int _damage;
		[SerializeField] private float _radius;
		[SerializeField] private LayerMask _layerMask;
		[SerializeField] private float _cameraShake;

		public int Damage => _damage;
		public float Radius => _radius;
		public LayerMask LayerMask => _layerMask;
		public float CameraShake => _cameraShake;
	}
}
