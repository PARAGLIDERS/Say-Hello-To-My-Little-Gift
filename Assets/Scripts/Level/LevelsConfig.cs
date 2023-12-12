using Music;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Level {
	[CreateAssetMenu(menuName = "Santa/Levels Config", fileName = "Levels Config")]
	public class LevelsConfig : ScriptableObject {
		[SerializeField] private List<LevelsConfigItem> _items;

		public List<LevelsConfigItem> Items => _items;

		private void OnValidate() {
			foreach (var item in _items) {
				item.Validate();
			}
		}
	}

	[Serializable]
	public class LevelsConfigItem {
		[SerializeField] private string _name;
		[SerializeField] private Sprite _icon;
		[SerializeField] private int _sceneIndex;
		[SerializeField] private MusicClipType _musicType;

		public string Name => _name;
		public Sprite Icon => _icon;
		public int SceneIndex => _sceneIndex;
		public MusicClipType MusicType => _musicType;

		[HideInInspector] public string ItemName;
		public void Validate() {
			ItemName = _name;
		}
	}
}
