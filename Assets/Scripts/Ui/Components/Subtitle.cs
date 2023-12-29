using TMPro;
using UnityEngine;

namespace Ui.Components {
	public class Subtitle : MonoBehaviour {
		[SerializeField] private TextMeshProUGUI _text;
		[SerializeField] private string[] _values;

		public TextMeshProUGUI Text => _text;

		public void Init() {
			int random = Random.Range(0, _values.Length);
			_text.text = _values[random];
		}
	}
}
