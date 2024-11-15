using System.Collections;
using UnityEngine;

namespace Root {
	public class CoroutineRunner : MonoBehaviour{
		public void Run(IEnumerator coroutine) {
			StartCoroutine(coroutine);
		}

		public void Stop(IEnumerator coroutine) {
			StopCoroutine(coroutine);
		}

		public void StopAll() {
			StopAllCoroutines();
		}
	}
}