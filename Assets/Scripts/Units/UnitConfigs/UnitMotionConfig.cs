using UnityEngine;

namespace Units.UnitConfigs {
	[CreateAssetMenu(menuName = "Unit Motion Config")]
	public class UnitMotionConfig : ScriptableObject {
		[SerializeField] private float _speed = 1f;
		[SerializeField] private float _maxSpeed = 10f;
		[SerializeField] private float _drag = 0.5f;
		
		public float Speed => _speed;
		public float MaxSpeed => _maxSpeed;
		public float Drag => _drag;
	}
}