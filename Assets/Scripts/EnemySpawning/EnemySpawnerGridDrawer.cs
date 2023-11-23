using System.Collections.Generic;
using UnityEngine;

namespace EnemySpawning {
	public class EnemySpawnerGridDrawer : MonoBehaviour {
		[SerializeField] private EnemySpawnerGridConfig _config;

		private EnemySpawnerGrid _grid;
		
		private void OnValidate() {
			if(_config == null) return;
			_grid ??= new EnemySpawnerGrid(_config);
		}

		private void OnDrawGizmosSelected() {
			if(_config == null) return;
			if(_grid == null) return;
			
			Gizmos.color = Color.red;
			List<Vector3> points = _grid.CalculatePoints();
			foreach (Vector3 point in points) {
				Gizmos.DrawCube(point, Vector3.one * (_config.CellSize * 0.9f));
			}
		}
	}
}