using UnityEngine;

namespace DayNightCycle {
    [CreateAssetMenu(menuName ="day night config")]
    public class DayNightConfig : ScriptableObject {
        [SerializeField, Range(0f, 1f)] private float _startPosition;
        [SerializeField] private AnimationCurve _sunRotation;
        [SerializeField] private AnimationCurve _sunIntensity;
        [SerializeField] private Gradient _sunColor;
        [SerializeField] private Gradient _skyColor;
        [SerializeField] private float _speed;

        public float StartPosition => _startPosition;
        public AnimationCurve Rotation => _sunRotation;
        public AnimationCurve Intensity => _sunIntensity;
        public Gradient SunColor => _sunColor;
        public Gradient SkyColor => _skyColor;
        public float Speed => _speed;
    }
}
