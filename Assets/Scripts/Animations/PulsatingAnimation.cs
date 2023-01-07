using DG.Tweening;
using UnityEngine;

namespace Animations {
	public class PulsatingAnimation : MonoBehaviour {
		[SerializeField] private float _upscale = 1.5f;
		
		private void Awake() {
			transform.DOScale(transform.localScale * _upscale, .3f).SetLoops(-1, LoopType.Yoyo);
		}
	}
}