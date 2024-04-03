using DamageSystem;
using DG.Tweening;
using Root;
using UnityEngine;

namespace Enemies {
	public class Swing : MonoBehaviour {
		[SerializeField] private TrailRenderer _trail;
		[SerializeField] private Transform _swing;
		[SerializeField] private Vector3 _targetRotation;
		[SerializeField] private float _time;
		[SerializeField] private Explosion _explosion;

		private Vector3 _initialRotation;
		private Sequence _sequence;

		private void Awake() {
			_trail.enabled = false;
			_initialRotation = _swing.localEulerAngles;
		}

		public void Activate() {
			_trail.enabled = false;
			_swing.localEulerAngles = _initialRotation;

			_trail.Clear();
			_trail.enabled = true;

			_sequence?.Kill();
			_sequence = DOTween.Sequence()
				.Insert(0f, _swing.DOLocalRotate(_targetRotation, _time))
				.InsertCallback(_time + _trail.time, () => _trail.enabled = false);

			_explosion.Activate();
			Core.SfxController.Play(SfxSystem.SfxType.EnemySnowmanThrow, transform.position);
		}
	}
}
