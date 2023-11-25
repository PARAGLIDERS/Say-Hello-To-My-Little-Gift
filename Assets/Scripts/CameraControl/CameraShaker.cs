using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CameraControl {
	public class CameraShaker : MonoBehaviour {
		private static Action<float> _shakeAction;

		public static void Shake(float magnitude = .1f) {
			_shakeAction?.Invoke(magnitude);
		}

		private void OnEnable() {
			_shakeAction += Shake_Internal;
		}

		private void OnDisable() {
			_shakeAction -= Shake_Internal;
		}
		
		private void Shake_Internal(float magnitude) {
			transform.DOComplete();
			transform.DOShakePosition(.5f, magnitude);
			//transform.DOShakeRotation(.3f, magnitude);

			//transform.localPosition += Random.insideUnitSphere * magnitude;
		}

		private void Update() {
			//transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, Time.deltaTime * 25f);
		}

        private void OnDestroy() {
            transform.DOKill();
        }
    }
}