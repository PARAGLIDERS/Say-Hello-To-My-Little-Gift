using UnityEngine;

namespace Units {
    public class UnitRotation {
        private readonly Transform _transform;
        private readonly float _speed;

        public UnitRotation(Transform transform, float speed) {
            _transform = transform;
            _speed = speed;
        }

        public void Update(Vector3 target) {
            Quaternion rotation = Quaternion.LookRotation(target - _transform.position);
            _transform.rotation = Quaternion.Lerp(_transform.rotation, rotation, Time.deltaTime * _speed);
        }

        public bool IsLookingAt(Vector3 target, float threshold = 0.03f) {
            Vector3 forward = _transform.forward;
            Vector3 toTarget = (target - _transform.position).normalized;

            return 1f - Vector3.Dot(forward, toTarget) <= threshold;
        }
    }
}
