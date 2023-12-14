using UnityEngine;

namespace Bullets {
	[CreateAssetMenu(menuName ="Santa/Bullets/Bullet Config")]
	public class BulletConfig : BulletBaseConfig {
		[SerializeField] private float _speedMin;
		[SerializeField] private float _speedMax;

		public float SpeedMin => _speedMin;
		public float SpeedMax => _speedMax;
	}
}
