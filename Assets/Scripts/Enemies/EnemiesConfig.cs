using System;
using System.Collections.Generic;
using EnemySpawning;
using Units.UnitConfigs;
using UnityEngine;

namespace Enemies {
	[CreateAssetMenu(menuName = "Enemies Config")]
	public class EnemiesConfig : ScriptableObject {
		[SerializeField] private List<EnemyConfig> _enemyConfigs;
		public List<EnemyConfig> EnemyConfigs => _enemyConfigs;
	}

	[Serializable]
	public struct EnemyConfig {
		[SerializeField] private EnemyType _type;
		[SerializeField] private Mesh _model;
		[SerializeField] private Vector3 _offset;
		[SerializeField] private UnitAnimationConfig _animationConfig;
		[SerializeField] private UnitMotionConfig _motionConfig;
		[SerializeField] private UnitRotationConfig _rotationConfig;

		public EnemyType Type => _type;
		public Mesh Model => _model;
		public Vector3 Offset => _offset;
		public UnitAnimationConfig AnimationConfig => _animationConfig;
		public UnitMotionConfig MotionConfig => _motionConfig;
		public UnitRotationConfig RotationConfig => _rotationConfig;
	}
}