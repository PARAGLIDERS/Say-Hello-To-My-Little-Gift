using System.Collections.Generic;
using UnityEngine;

namespace Grid {
	public class SpawnerGridDrawer : MonoBehaviour {
		[SerializeField] private SpawnerGridConfig _config;

        private SpawnerGridConfig _configCurrent;
		private SpawnerGrid _grid;

		private void OnValidate() {
			if(_config == null) return;
            if(_configCurrent == _config) return;

            _configCurrent = _config;
			_grid = new SpawnerGrid(_config);
		}

		private void OnDrawGizmos() {
			if(_config == null) return;
            if (_grid == null) return;

            _grid.CalculatePoints();

			Gizmos.color = Color.red;
			List<Vector3> points = _grid.Points;
			foreach (Vector3 point in points) {
				Gizmos.DrawCube(point, Vector3.one * (_config.CellSize * 0.9f));
			}
		}
	}
}