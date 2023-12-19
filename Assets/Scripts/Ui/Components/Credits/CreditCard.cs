using TMPro;
using UnityEngine;

namespace Ui.Components {
	public class CreditCard : MonoBehaviour {
		[SerializeField] private TextMeshProUGUI _title;
		[SerializeField] private TextMeshProUGUI _description;

		public void Init(CreditsConfigItem config) {
			_title.text = config.Title;

			string description = config.Description;
			string[] lines = description.Split('\n');

			description = string.Empty;
			for (int i = 0; i < lines.Length; i++) {
				description += $"<sprite=0>{lines[i]}\n";
			}

			_description.text = description;
		}
	}
}
