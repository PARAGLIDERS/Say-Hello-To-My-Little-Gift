using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies {
	[Serializable]
	public class EnemyStage {
		[SerializeField] private List<int> _stages;

		private int _currentIndex;

		public void Reset() {
			_currentIndex = 0;
		}

		public bool IsChanged(float value) {
			int index = _currentIndex;

			for (int i = _currentIndex; i < _stages.Count; i++) {
				index = i;

				if (_stages[i] < value * 100) {
					break;
				}
			}

			if(index == _currentIndex) {
				return false;
			}

			_currentIndex = index;
			return true;
		}
	}
}
