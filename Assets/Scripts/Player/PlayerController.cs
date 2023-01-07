using Units;
using UnityEngine;

namespace Player {
	public class PlayerController : UnitRigidbody {
		public static Vector3 POSITION;
		private InputHandler _inputHandler;

		private void Awake() {
			InitUnit();
			_inputHandler = new InputHandler(Camera.main);
			Cursor.visible = false;
			Application.targetFrameRate = 75;
		}

		protected override void Update() {
			base.Update();
			POSITION = transform.position;
		}

		private void FixedUpdate() {
			Rotate(_inputHandler.GetPointerPosition());
			Move(_inputHandler.GetInput());
		}
	}
}