
using UnityEngine;

namespace Units {
	public class UnitRigidbody : Unit {
		[SerializeField] private UnitConfig _config;
		[SerializeField] private Transform _unitTransform;
		[SerializeField] private Transform _bodyTransform;
		[SerializeField] private Rigidbody _rigidbody;
		
		private UnitRotation _rotation;

		protected override void InitUnit() {
			base.InitUnit();
			_rotation = new UnitRotation(_config.RotationConfig, _unitTransform);
		}
		
		public override UnitAnimation GetAnimation() => new UnitAnimation(_config.AnimationConfig, _bodyTransform);
		public override UnitMotion GetMotion() => new UnitMotionRigidbody(_config.MotionConfig, _rigidbody);

		public void Rotate(Vector3 direction) {
			_rotation.Rotate(direction);
		}
	}
}