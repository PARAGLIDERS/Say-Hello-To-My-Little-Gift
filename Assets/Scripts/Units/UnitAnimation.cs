using Misc.Root;
using PoolSystem;
using Units.UnitConfigs;
using UnityEngine;

namespace Units {
	public class UnitAnimation : MonoBehaviour {
        [SerializeField] private float _playSpeed = 3f;
        [SerializeField] private AnimationCurve _jumpCurve;
        [SerializeField] private AnimationCurve _scaleCurve;
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
			
			_body.localPosition = new Vector3(0f, _jumpCurve.Evaluate(1f - _playTimer), 0f);
			_body.localScale = new Vector3(1f, 1f + _scaleCurve.Evaluate(1f - _playTimer), 1f);

			_playTimer -= Time.deltaTime * _playSpeed;

			if (_playTimer <= 0f) {
				_isPlayin = false;
				Core.PoolController.Spawn(PoolType.Steps, _body.position, _body.rotation);
			}
		}
	}
}