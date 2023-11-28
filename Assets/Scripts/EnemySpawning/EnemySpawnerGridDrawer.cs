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
			if(_grid == null) return;
            _grid.CalculatePoints();

			Gizmos.color = Color.red;
			List<Vector3> points = _grid.Points;
			foreach (Vector3 point in points) {
				Gizmos.DrawCube(point, Vector3.one * (_config.CellSize * 0.9f));
			}
		}
	}
}