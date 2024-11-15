using Root;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Grid {
    public class SpawnerGrid {
        public List<Vector3> Points { get; private set; }
        private readonly SpawnerGridConfig _config;

        private const float _distanceToPlayer = 20f;

        public SpawnerGrid(SpawnerGridConfig config) { 
            _config = config;
        }

        public Vector3 GetPosition() {
            if (Points == null) {
                Debug.LogError("spawner grid is not calculated");
                return Vector3.zero;
            }

            if (Points.Count == 0) {
                Debug.LogError("spawner grid is empty");
                return Vector3.zero;
            }

            List<Vector3> filteredPoints = GetFilteredPoints();

            if(filteredPoints.Count > 0) {
                return filteredPoints.Random();
            }

            return Points.Random();
        }

        public void CalculatePoints() {
            Points = new List<Vector3>();

            Vector2 gridSize = _config.GridSize;
            float cellSize = _config.CellSize;
            float gridHeight = _config.GridHeight;

            for (int i = -(int)gridSize.x; i < gridSize.x; i++) {
                for (int j = -(int)gridSize.y; j < gridSize.y; j++) {
                    if (IsInner(new Vector2(i, j)))
                        continue;

                    Vector3 point = new(i * cellSize, gridHeight, j * cellSize);
                    if (!Physics.CheckBox(point, Vector3.one * cellSize / 2f, Quaternion.identity, _config.ObstacleLayer)) {
                        Points.Add(point);
                    }
                }
            }
        }

        private bool IsInner(Vector2 point) {
            Vector2 gridInnerSize = _config.GridInnerSize;

            Vector2 innerMin = new(-gridInnerSize.x, -gridInnerSize.y);
            Vector2 innerMax = new(gridInnerSize.x, gridInnerSize.y);

            bool x = point.x >= innerMin.x && point.x <= innerMax.x;
            bool y = point.y >= innerMin.y && point.y <= innerMax.y;

            return x && y;
        }

        private List<Vector3> GetFilteredPoints() {
			Vector3 playerPosition = Core.LevelController.Player.Position;

			List<Vector3> filteredPoints = new List<Vector3> ();

            for (int i = 0; i < Points.Count; i++) {
                if (Vector3.Distance(Points[i], playerPosition) > _distanceToPlayer) {
                    filteredPoints.Add(Points[i]);
                }
            }

            return filteredPoints;
		}
    }
}
