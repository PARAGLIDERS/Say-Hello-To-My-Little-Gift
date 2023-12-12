using UnityEngine;

namespace Ui {
	public abstract class UiScreen : MonoBehaviour {
		public abstract void Init();
		public virtual void Enter() { }
		public virtual void Exit() { }
	}
}