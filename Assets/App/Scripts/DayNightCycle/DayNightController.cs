using UnityEngine;

namespace DayNightCycle {
    public class DayNightController {
        private readonly DayNightConfig _config;
        private readonly Light _sun;
        
        private float _time;

        public DayNightController(Transform parent, DayNightConfig config) { 
            _config = config;            
            _sun = new GameObject("sun").AddComponent<Light>();

            _sun.transform.SetParent(parent);
            _sun.type = LightType.Directional;
            _sun.shadows = LightShadows.Soft;

            _sun.gameObject.SetActive(false);
        }

        public void Start() {
            _time = _config.StartPosition;
            _sun.gameObject.SetActive(true);
            RenderSettings.sun = _sun;
        }

        public void Stop() {
            _sun.gameObject.SetActive(false);
        }

        public void Update() {
            _time += Time.deltaTime * _config.Speed;
            if(_time > 1f) _time = 0f;

            float rotationY = _time * 180f;
            float rotationX = _config.Rotation.Evaluate(_time) * 50f;
            _sun.transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
            _sun.intensity = _config.Intensity.Evaluate(_time) * 1.5f;
            _sun.color = _config.SunColor.Evaluate(_time);
            RenderSettings.ambientSkyColor = _config.SkyColor.Evaluate(_time);
        }
    }
}
