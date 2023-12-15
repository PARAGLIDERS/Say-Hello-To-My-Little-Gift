using DG.Tweening;
using UnityEngine;

namespace Enemies {
	public class Swing : MonoBehaviour {
		[SerializeField] private TrailRenderer _trail;
		[SerializeField] private Transform _swing;
		[SerializeField] [Range(0f, 360)] private float _angle;
		[SerializeField] private float _time;
		[SerializeField] private int _damage;
		[SerializeField] private float _radius;



		private void Awake() {
			_trail.enabled = false;
		}

		public void Activate() {
			_trail.enabled = true;
			_trail.Clear();


		}

		private void OnDrawGizmos() {
			
		}
	}
}
