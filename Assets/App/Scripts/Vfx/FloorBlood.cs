using PoolSystem;
using UnityEngine;

namespace Vfx {
    class FloorBlood : PoolObject {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private float _lifetime = 25;

        private float _currentLifetime;

        private void OnValidate() {
            if (_lifetime <= 0) {
                _lifetime = 0;
            }
        }

        public override void Activate(Vector3 position, Quaternion rotation) {
            _currentLifetime = Time.time + _lifetime;
            _renderer.transform.rotation = Quaternion.Euler(90f, Random.Range(-360f, 360f), 0f);
            base.Activate(position, rotation);
        }

        private void Update() {
            float alpha = (_currentLifetime - Time.time) / _lifetime;
            _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, alpha);

            if(Time.time >= _currentLifetime) {
                Deactivate();
            }
        }
    }
}
