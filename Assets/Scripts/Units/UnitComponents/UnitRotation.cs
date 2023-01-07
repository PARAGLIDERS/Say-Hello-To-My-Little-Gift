using Units.UnitConfigs;
using UnityEngine;

namespace Units {
	public class UnitRotation {
		private readonly UnitRotationConfig _config;
		private readonly Transform _transform;
		
		public UnitRotation(UnitRotationConfig config, Transform transform) {
			_config = config;
			_transform = transform;
		}
		
		public void Rotate(Vector3 point) {
			Quaternion rotation = Quaternion.LookRotation(point - _transform.position);
			_transform.rotation = Quaternion.Lerp(_transform.rotation, rotation, Time.deltaTime * _config.RotationSpeed);
		}
	}
}