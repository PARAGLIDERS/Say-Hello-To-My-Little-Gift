using CameraControl;
using UnityEngine;

namespace Root {
    public class InputController {
        private readonly Plane _floor;
        private CameraController _camera => Core.LevelController.Camera;

        public InputController() {
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
