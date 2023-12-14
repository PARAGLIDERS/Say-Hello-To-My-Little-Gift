using Root;
using PoolSystem;
using UnityEngine;
using Units.UnitConfigs;

namespace Units {
	public class UnitAnimation : MonoBehaviour {
		[SerializeField] private UnitAnimationConfig _config;
		[SerializeField] private Transform _body;
		
		private float _playTimer;
		private bool _isPlayin;
        		
		public void Trigger() {
			if(_isPlayin) return;
			_playTimer = 1f;
			_isPlayin = true;
		}
		
		private void Update() {
			if(!_isPlayin) return;
			
			_body.localPosition = new Vector3(0f, _config.JumpCurve.Evaluate(1f - _playTimer), 0f);
			_body.localScale = new Vector3(1f, 1f + _config.ScaleCurve.Evaluate(1f - _playTimer), 1f);

			_playTimer -= Time.deltaTime * _config.AnimationSpeed;

			if (_playTimer <= 0f) {
				_isPlayin = false;
				Core.PoolController.Spawn(PoolType.VFX_Steps, _body.position, _body.rotation);
			}
		}
	}
}