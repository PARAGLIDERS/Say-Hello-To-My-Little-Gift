using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemySpawning {
	public class EnemySpawnerGrid {
		private readonly List<Vector3> _points;
		private readonly EnemySpawnerGridConfig _config;
		
		public EnemySpawnerGrid(EnemySpawnerGridConfig config) {
			_config = config;
			_points = CalculatePoints();
		}
		
		public Vector3 GetPosition() {
			return _points[Random.Range(0, _points.Count)];
		}

		public List<Vector3> CalculatePoints() {
			List<Vector3> points = new ();
			
			Vector2 gridSize = _config.GridSize;
			float cellSize = _config.CellSize;
			float gridHeight = _config.GridHeight;
			
			for (int i = -(int)gridSize.x; i < gridSize.x; i++) {
				for (int j = -(int)gridSize.y; j < gridSize.y; j++) {
					if(IsInner(new Vector2(i, j))) continue;
					
					Vector3 point = new (i * cellSize, gridHeight, j * cellSize);
					if (!Physics.CheckBox(point, Vector3.one * cellSize / 2f, Quaternion.identity, _config.ObstacleLayer)) {
						points.Add(point);
					}
				}
			}

			return points;
		}
		
		private bool IsInner(Vector2 point) {
			Vector2 gridInnerSize = _config.GridInnerSize;
			
			Vector2 innerMin = new(-gridInnerSize.x, -gridInnerSize.y);
			Vector2 innerMax = new(gridInnerSize.x, gridInnerSize.y);

			bool x = point.x >= innerMin.x && point.x <= innerMax.x;
			bool y = point.y >= innerMin.y && point.y <= innerMax.y;

			return x && y;
		}
	}
}