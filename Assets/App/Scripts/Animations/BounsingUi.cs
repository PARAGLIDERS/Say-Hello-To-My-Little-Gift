using DG.Tweening;
using UnityEngine;

namespace Animations {
	public class BounsingUi : MonoBehaviour{
		[SerializeField] private float _magnitude = 1.05f;
		[SerializeField] private float _period = 1;

		private void Awake() {
			transform
				.DOScale(_magnitude, _period)
				.SetEase(Ease.InOutSine)
				.SetLoops(-1, LoopType.Yoyo)
				.SetUpdate(true);
		}
	}
}
