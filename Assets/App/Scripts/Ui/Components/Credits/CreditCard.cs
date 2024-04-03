using DG.Tweening;
using Root;
using UnityEngine;

namespace Ui.Components {
	public class CreditCard : MonoBehaviour {
		[SerializeField] private RectTransform _description;

		public Tweener GetAnim() {
			return _description.DOAnchorPosY(200f, Constants.UiAnimDuration)
				.From(true).SetEase(Ease.OutSine);
		}
	}
}
