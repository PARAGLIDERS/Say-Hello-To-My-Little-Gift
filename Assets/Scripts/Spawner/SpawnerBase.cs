using Grid;
using Root;
using System.Collections;

namespace Spawner {
	public abstract class SpawnerBase {
		protected IEnumerator _execution;
		protected SpawnerGrid _grid;

		public void Start(SpawnerGridConfig gridConfig) {
			_grid = new SpawnerGrid(gridConfig);
			_grid.CalculatePoints();
			_execution = Execute();
			Core.CoroutineRunner.Run(_execution);
		}

		public void Stop() {
			if (_execution == null)
				return;
			Core.CoroutineRunner.Stop(_execution);
		}

		protected abstract IEnumerator Execute();
	}
}
