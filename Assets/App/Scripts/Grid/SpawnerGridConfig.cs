using UnityEngine;

namespace Grid {
    [CreateAssetMenu(menuName = "Santa/Grid/Spawner Grid Config", fileName = "Spawner Grid Config")]
    public class SpawnerGridConfig : ScriptableObject {
        [SerializeField] private float _cellSize = 5f;
        [SerializeField] private Vector2 _gridSize = new(10, 10);
        [SerializeField] private Vector2 _gridInnerSize = new(5, 5);
        [SerializeField] private float _gridHeight = 1f;
        [SerializeField] private LayerMask _obstaclesLayer;

        public float CellSize => _cellSize;
        public Vector2 GridSize => _gridSize;
        public Vector2 GridInnerSize => _gridInnerSize;
        public float GridHeight => _gridHeight;
        public LayerMask ObstacleLayer => _obstaclesLayer;
    }
}
