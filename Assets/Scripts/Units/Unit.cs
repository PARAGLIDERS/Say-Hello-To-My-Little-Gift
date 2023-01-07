using UnityEngine;

namespace Units {
	public abstract class Unit : MonoBehaviour {
		private UnitAnimation _animation;
		private UnitMotion _motion;

		public abstract UnitAnimation GetAnimation();
		public abstract UnitMotion GetMotion();
		
		protected virtual void InitUnit() {
			_animation = GetAnimation();
			_motion = GetMotion();
		}

		public void Move(Vector3 direction) {
			if (direction == Vector3.zero) return;
			
			_motion.Move(direction);
			_animation.Trigger();
		}

		protected virtual void Update() {
			_animation.Update();
		}
	}
}