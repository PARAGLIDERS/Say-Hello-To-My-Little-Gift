using UnityEngine;

namespace Units.UnitConfigs {
	[CreateAssetMenu(menuName = "Unit Animation Config")]
	public class UnitAnimationConfig : ScriptableObject {
		[SerializeField] private float _animationSpeed = 3f;
		[SerializeField] private AnimationCurve _jumpCurve;
		[SerializeField] private AnimationCurve _scaleCurve;
		
		public float AnimationSpeed => _animationSpeed;
		public AnimationCurve JumpCurve => _jumpCurve;
		public AnimationCurve ScaleCurve => _scaleCurve;
	}
}