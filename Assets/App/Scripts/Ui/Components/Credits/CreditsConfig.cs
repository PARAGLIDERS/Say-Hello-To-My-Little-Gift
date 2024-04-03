using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ui.Components {
	[CreateAssetMenu(menuName = "Santa/Credits Config")]
	public class CreditsConfig : ScriptableObject {
		[SerializeField] private List<CreditsConfigItem> _items;
		
		public List<CreditsConfigItem> Items => _items;
	}

	[Serializable]
	public struct CreditsConfigItem {
		[SerializeField] private string _title;
		[SerializeField] [TextArea(5, 10)] private string _description;

		public string Title => _title;
		public string Description => _description;
	}
}
