using Misc.Root;
using PoolSystem;
using Units.UnitConfigs;
using UnityEngine;

namespace Units {
	public class UnitAnimation {
		private readonly UnitAnimationConfig _config;
		private readonly Transform _transform;
		
		private float _animationTimer;
		private bool _isPlayin;

		public UnitAnimation(UnitAnimationConfig config, Transform transform) {
			_config = config;
			_transform = transform;
		}
		
		public void Trigger() {
			if(_isPlayin) return;
			_animationTimer = 1f;
			_isPlayin = true;
		}
		
		public void Update() {
			if(!_isPlayin) return;
			
			_transform.localPosition = new Vector3(0f, _config.JumpCurve.Evaluate(1f - _animationTimer), 0f);
			_transform.localScale = new Vector3(1f, 1f + _config.ScaleCurve.Evaluate(1f - _animationTimer), 1f);

			_animationTimer -= Time.deltaTime * _config.AnimationSpeed;

			if (_animationTimer <= 0f) {
				_isPlayin = false;
				Core.PoolController.Spawn(PoolType.Steps, _transform.position, _transform.rotation);
			}
		}
	}
}