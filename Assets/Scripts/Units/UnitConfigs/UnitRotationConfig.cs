using UnityEngine;

namespace Units.UnitConfigs {
	[CreateAssetMenu(menuName = "Unit Rotation Config")]
	public class UnitRotationConfig : ScriptableObject {
		[SerializeField] private float _rotationSpeed = 15f;
		
		public float RotationSpeed => _rotationSpeed;
	}
}