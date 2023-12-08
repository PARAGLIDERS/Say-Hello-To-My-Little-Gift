using UnityEngine;

namespace Animations {
	public class BouncingAnimation : MonoBehaviour {
		[SerializeField] private AnimationCurve _curve;
		[SerializeField] private float _curveFactor = 1;
		[SerializeField] [Range(0.1f, 10)] private float _period = 3;

		private Vector3 _initialPosition;
		private float _time;

		private void Awake() {
			_initialPosition = transform.localPosition;
		}

		private void Update() {
			_time += Time.deltaTime;
			if(_time >= _period) _time = 0;

			float t = _time / _period;
			float shift = _curve.Evaluate(t) * _curveFactor;

			transform.localPosition = _initialPosition + Vector3.up * shift;
		}
	}
}
