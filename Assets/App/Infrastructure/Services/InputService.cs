using CameraControl;
using UnityEngine;

namespace Root {
    public class InputService {
        private readonly Plane _floor;
        private readonly CameraController _camera;

        public InputService(CameraController camera) {
            _camera = camera;
            _floor = new (Vector3.up, 0);
        }

        public Vector3 GetPointerPosition() {
            Vector3 pointerPosition = Vector3.zero;

            Ray cameraRay = _camera.ScreenPointToRay(Input.mousePosition);

            if (_floor.Raycast(cameraRay, out float distance)) {
                pointerPosition = cameraRay.GetPoint(distance);
            }

            return pointerPosition;
        }

        public bool GetEscapeInput() => Input.GetKeyDown(KeyCode.Escape);
        public bool GetGunWheelInput() => Input.GetKey(KeyCode.Q);
        public bool GetPlayerDashInput() => Input.GetKeyDown(KeyCode.Space);

		public Vector3 GetPlayerInput() {
            Vector3 right = GetAxis("Horizontal", _camera.transform.right);
            Vector3 forward = GetAxis("Vertical", _camera.transform.forward);

            return (forward + right).normalized;
        }

        private Vector3 GetAxis(string axisName, Vector3 orientation) {
            float axisValue = Input.GetAxisRaw(axisName);
            Vector3 orientationProjected = Vector3.ProjectOnPlane(orientation, Vector3.up);

            return axisValue * orientationProjected;
        }
    }
}
