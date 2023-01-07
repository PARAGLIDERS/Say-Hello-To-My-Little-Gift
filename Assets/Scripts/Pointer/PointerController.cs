using Player;
using UnityEngine;

namespace Pointer {
	public class PointerController : MonoBehaviour {
		private InputHandler _inputHandler;
		
		private void Awake() {
			_inputHandler = new InputHandler(Camera.main);
		}

		private void Update() {
			transform.position = _inputHandler.GetPointerPosition();
		}
	}
}