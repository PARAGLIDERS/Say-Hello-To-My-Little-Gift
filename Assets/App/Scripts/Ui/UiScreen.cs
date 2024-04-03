using UnityEngine;

namespace Ui {
	public abstract class UiScreen : MonoBehaviour {
		public abstract void Init();
		public virtual void Enter() { 
			PlayEnterAnim();
		}

		public virtual void Exit() { }
		protected virtual void PlayEnterAnim() { }
	}
}