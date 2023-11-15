using System;
using Units;
using UnityEngine;

namespace Enemies {
	[CreateAssetMenu(menuName = "Enemy Config")]
	public class EnemyConfig : UnitConfig {
		[SerializeField] private Mesh _model;
		[SerializeField] private Vector3 _offset;

		public Mesh Model => _model;
		public Vector3 Offset => _offset;
	}

	[Serializable]
	public struct EnemyMesh {
		
	}
}