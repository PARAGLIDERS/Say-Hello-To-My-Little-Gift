using Units.UnitConfigs;
using UnityEngine;

namespace Units {
	[CreateAssetMenu(menuName = "Unit Config")]
	public class UnitConfig : ScriptableObject {
		[SerializeField] private UnitAnimationConfig _animationConfig;
		[SerializeField] private UnitMotionConfig _motionConfig;
		[SerializeField] private UnitRotationConfig _rotationConfig;

		public UnitAnimationConfig AnimationConfig => _animationConfig;
		public UnitMotionConfig MotionConfig => _motionConfig;
		public UnitRotationConfig RotationConfig => _rotationConfig;
	}
}