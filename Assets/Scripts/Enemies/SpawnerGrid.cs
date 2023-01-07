using System.Collections.Generic;
using UnityEngine;

namespace Enemies {
	public class SpawnerGrid : MonoBehaviour {
		[SerializeField] private float _cellSize = 5f;
		[SerializeField] private Vector2 _gridSize = new(10, 10);
		[SerializeField] private Vector2 _gridInnerSize = new(5, 5);
		[SerializeField] private float _gridHeight = 1f;
		[SerializeField] private LayerMask _obstaclesLayer;

		private List<Vector3> _points = new ();

		public Vector3 GetPosition() {
			return _points[Random.Range(0, _points.Count)];
		}
		
		public void Init() {
			for (int i = -(int)_gridSize.x; i < _gridSize.x; i++) {
				for (int j = -(int)_gridSize.y; j < _gridSize.y; j++) {
					if(IsInner(new Vector2(i, j))) continue;
					
					Vector3 point = new (i * _cellSize, _gridHeight, j * _cellSize);
					if (!Physics.CheckBox(point, Vector3.one * _cellSize / 2f, Quaternion.identity, _obstaclesLayer)) {
						_points.Add(point);
					}
				}
			}
		}

		private bool IsInner(Vector2 point) {
			Vector2 innerMin = new(-_gridInnerSize.x, -_gridInnerSize.y);
			Vector2 innerMax = new(_gridInnerSize.x, _gridInnerSize.y);

			bool x = point.x >= innerMin.x && point.x <= innerMax.x;
			bool y = point.y >= innerMin.y && point.y <= innerMax.y;

			return x && y;
		}
		
		private void OnDrawGizmos() {
			Gizmos.color = new Color(1, 0, 0, 0.3f);

			foreach (Vector3 point in _points) {
				Gizmos.DrawCube(point, Vector3.one * (_cellSize * 0.9f));
			}
		}
	}
}