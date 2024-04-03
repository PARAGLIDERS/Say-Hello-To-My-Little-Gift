using UnityEngine;

namespace Bullets {
	[CreateAssetMenu(menuName = "Santa/Bullets/ProjectileConfig")]
	public class ProjectileConfig : BulletBaseConfig {
		[SerializeField] private float _force;
		[SerializeField] private float _gravity;

		public float Force => _force;
		public float Gravity => _gravity;
	}
}
