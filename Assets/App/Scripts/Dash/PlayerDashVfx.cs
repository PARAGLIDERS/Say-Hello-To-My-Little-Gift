using System.Collections.Generic;
using UnityEngine;

namespace Dash {
	public class PlayerDashVfx : MonoBehaviour {
		[SerializeField] private List<TrailRenderer> trails;
		[SerializeField] private float _lifeTime;

		private float _time;

		public void Init() {
			Deactivate();
			_time = 0;
		}

		public void Activate() {			
			_time = Time.time + _lifeTime;
			foreach (var trail in trails) {
				trail.Clear();
				trail.enabled = true;
			}
		}

		public void Deactivate() {
			foreach (var trail in trails) {
				trail.enabled = false;
			}
		}

		private void Update() {
			if(Time.time < _time) return;
			Deactivate();
		}
	}
}
