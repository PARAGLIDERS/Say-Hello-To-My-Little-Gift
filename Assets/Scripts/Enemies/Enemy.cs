using Player;
using Units;
using UnityEngine;

namespace Enemies {
	public class Enemy : UnitNavmesh {
		[SerializeField] private MeshFilter _modelMeshFilter;
		[SerializeField] private MeshRenderer _modelMeshRenderer;
		[SerializeField] private EnemyModelConfig[] _enemyModelConfigs;
		
		public void OnEnable() {
			EnemyModelConfig config = _enemyModelConfigs[Random.Range(0, _enemyModelConfigs.Length)];
			
			_modelMeshFilter.mesh = config.Model;
			_modelMeshRenderer.transform.localPosition = config.Offset;
			
			Init(config);
		}

		protected override void Update() {
			base.Update();
			Move(PlayerController.POSITION);
		}
	}
}